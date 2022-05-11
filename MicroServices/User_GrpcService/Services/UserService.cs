using Core.EFCore.Models;
using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using User_GrpcService.Models;

namespace User_GrpcService.Services
{
    public class UserService : UserGRPCService.UserGRPCServiceBase
    {
        private readonly ILogger<UserService> _logger;

        private readonly IBaseContextWrapper<IUser> _context;

        private readonly IClientFactory _clientFactory;

        private readonly AuthService _authService;

        public UserService(ILogger<UserService> logger, IBaseContextWrapper<IUser> context, IClientFactory clientFactory)
        {
            _logger = logger;
            _context = context;
            _clientFactory = clientFactory;
            _authService = new AuthService();
        }

        public override async Task<UserModel> RegUser(RegUserRequest request, ServerCallContext context)
        {
            if (_authService.CheckUsername(request.UserName))
            {
                return null;
            }

            var model = new User(request.Name, request.Phone);
            
            var res = await _context.Add(model);
            //Task.WaitAll();

            if (res == null)
            {
                return null;
            }

            await Task.Run(() => _authService.RegisterUser(res.Id, request.UserName, request.Password));

            return new UserModel()
            {
                Id = res.Id,
                Name = res.Name,
                Phone = res.Phone
            };
        }

        public override async Task<UserModel> AuthUser(AuthUserRequest request, ServerCallContext context)
        {
            var res = await Task.Run(() => _authService.AuthUser(request.UserName, request.Password));

            var user = await _context.GetById(res);

            if (res == -1 || user == null)
            {
                return null;
            }

            return new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone
            };
        }

        public override async Task GetUsers(GetUsersRequest request, IServerStreamWriter<UserModel> responseStream, ServerCallContext context)
        {
            foreach (var response in await _context.Get())
            {
                var model = new UserModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Phone = response.Phone
                };

                await responseStream.WriteAsync(model);
            }
        }

        public override async Task<UserModel> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var temp = await _context.GetById(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new UserModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                Phone = temp.Phone
            };

            return res;
        }

        public override async Task<UserModel> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var temp = await _context.Remove(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new UserModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                Phone = temp.Phone
            };

            return res;
        }

        public override async Task<NullResponse> NotifyUser(NotifyUserRequest request, ServerCallContext context)
        {
            var orderService = _clientFactory.GetOrderServiceClient();

            var temp = await _context.GetById(request.UserId);
            var orderModel = await orderService.GetOrderById(request.OrderId);

            //Task.WaitAll();

            if (temp == null || orderModel == null)
            {
                return null;
            }

            var order = new Order_DAL()
            {
                Id = orderModel.Id,
                IdStation = orderModel.IdStation,
                IdUser = orderModel.IdUser,
                Name = orderModel.Name,
                CreatedAt = orderModel.CreatedAt,
                Closed = orderModel.Closed,
                CompletedWork = orderModel.CompletedWork
            };

            temp.OrderCompleted(order);

            return new NullResponse();
        }
    }
}
