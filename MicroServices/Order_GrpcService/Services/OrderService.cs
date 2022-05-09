using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
using Order_GrpcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_GrpcService.Services
{
    public class OrderService : OrderGRPCService.OrderGRPCServiceBase
    {
        private readonly ILogger<OrderService> _logger;

        private readonly IBaseContextWrapper<IOrder> _context;

        private readonly IClientFactory _clientFactory;

        public OrderService(ILogger<OrderService> logger, IBaseContextWrapper<IOrder> context, IClientFactory clientFactory)
        {
            _logger = logger;
            _context = context;
            _clientFactory = clientFactory;
        }

        public override async Task<OrderModel> AddOrder(AddOrderRequest request, ServerCallContext context)
        {
            var completedWork = new Dictionary<int, int>();
            foreach (var item in request.CompletedWork)
            {
                completedWork.Add(item.Key, item.Value);
            }
            var model = new OrderM(request.Name, request.IdStation, request.IdUser, request.IdCar, DateTime.Parse(request.CreatedAt), DateTime.MinValue, completedWork);
            var temp = await _context.Add(model);

            if (temp == null)
            {
                return null;
            }

            var res = new OrderModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                IdStation = temp.IdStation,
                IdUser = temp.IdUser,
                IdCar = temp.IdCar,
                CreatedAt = temp.CreatedAt.ToString(),
                Closed = temp.Closed.ToString()
            };

            foreach (var item in temp.CompletedWork)
            {
                res.CompletedWork.Add((int)item.Key, item.Value);
            }
            return res;
        }

        public override async Task<OrderModel> CloseOrder(CloseOrderRequest request, ServerCallContext context)
        {
            var order = await _context.GetById(request.Id);
            order.Closed = DateTime.Now;
            await _context.Put(order.Id, order);
            if (order == null)
            {
                return null;
            }

            var userClient = _clientFactory.GetUserServiceClient();
            var req = new NotifyUserRequest()
            {
                OrderId = request.Id,
                UserId = order.IdUser
            };
            await userClient.NotifyUserAsync(req);

            var res = new OrderModel()
            {
                Id = order.Id,
                Name = order.Name,
                IdStation = order.IdStation,
                IdUser = order.IdUser,
                IdCar = order.IdCar,
                CreatedAt = order.CreatedAt.ToString(),
                Closed = order.Closed.ToString()
            };

            foreach (var item in order.CompletedWork)
            {
                res.CompletedWork.Add((int)item.Key, item.Value);
            }
            return res;
        }

        public override async Task<OrderModel> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var temp = await _context.Remove(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new OrderModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                IdStation = temp.IdStation,
                IdUser = temp.IdUser,
                IdCar = temp.IdCar,
                CreatedAt = temp.CreatedAt.ToString(),
                Closed = temp.Closed.ToString()
            };

            foreach (var item in temp.CompletedWork)
            {
                res.CompletedWork.Add((int)item.Key, item.Value);
            }

            return res;
        }

        public override async Task<OrderModel> GetOrderById(GetOrderByIdRequest request, ServerCallContext context)
        {
            var temp = await _context.GetById(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new OrderModel()
            {
                Id = temp.Id,
                Name = temp.Name,
                IdStation = temp.IdStation,
                IdUser = temp.IdUser,
                IdCar = temp.IdCar,
                CreatedAt = temp.CreatedAt.ToString(),
                Closed = temp.Closed.ToString()
            };

            foreach (var item in temp.CompletedWork)
            {
                res.CompletedWork.Add((int)item.Key, item.Value);
            }

            return res;
            
        }

        public override async Task GetOrders(GetOrdersRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            foreach (var response in await _context.Get())
            {
                var completedWork = new Dictionary<int, int>();
                foreach (var item in response.CompletedWork)
                {
                    completedWork.Add((int)item.Key, item.Value);
                }
                var model = new OrderModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    IdStation = response.IdStation,
                    IdUser = response.IdUser,
                    IdCar = response.IdCar,
                    CreatedAt = response.CreatedAt.ToString(),
                    Closed = response.Closed.ToString()
                };

                model.CompletedWork.Add(completedWork);

                await responseStream.WriteAsync(model);
            }

        }

        public override async Task GetOrdersByCarId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            foreach (var response in (await _context.Get()).Where(OrderUI => OrderUI.IdCar == request.Id))
            {
                var completedWork = new Dictionary<int, int>();
                foreach (var item in response.CompletedWork)
                {
                    completedWork.Add((int)item.Key, item.Value);
                }
                var model = new OrderModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    IdStation = response.IdStation,
                    IdUser = response.IdUser,
                    IdCar = response.IdCar,
                    CreatedAt = response.CreatedAt.ToString(),
                    Closed = response.Closed.ToString()
                };

                model.CompletedWork.Add(completedWork);

                await responseStream.WriteAsync(model);
            }
        }

        public override async Task GetOrdersByOrderStationId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            foreach (var response in (await _context.Get()).Where(OrderUI => OrderUI.IdStation == request.Id))
            {
                var completedWork = new Dictionary<int, int>();
                foreach (var item in response.CompletedWork)
                {
                    completedWork.Add((int)item.Key, item.Value);
                }
                var model = new OrderModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    IdStation = response.IdStation,
                    IdUser = response.IdUser,
                    IdCar = response.IdCar,
                    CreatedAt = response.CreatedAt.ToString(),
                    Closed = response.Closed.ToString()
                };

                model.CompletedWork.Add(completedWork);

                await responseStream.WriteAsync(model);
            }
        }

        public override async Task GetOrdersByUserId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            foreach (var response in (await _context.Get()).Where(OrderUI => OrderUI.IdUser == request.Id))
            {
                var completedWork = new Dictionary<int, int>();
                foreach (var item in response.CompletedWork)
                {
                    completedWork.Add((int)item.Key, item.Value);
                }
                var model = new OrderModel()
                {
                    Id = response.Id,
                    Name = response.Name,
                    IdStation = response.IdStation,
                    IdUser = response.IdUser,
                    IdCar = response.IdCar,
                    CreatedAt = response.CreatedAt.ToString(),
                    Closed = response.Closed.ToString()
                };

                model.CompletedWork.Add(completedWork);

                await responseStream.WriteAsync(model);
            }
        }
    }
}
