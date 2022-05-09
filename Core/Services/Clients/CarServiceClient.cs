using Core.EFCore.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Clients
{
    public class CarServiceClient : ICarServiceClient
    {
        private readonly CarGRPCService.CarGRPCServiceClient _client;

        public CarServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _client = new CarGRPCService.CarGRPCServiceClient(channel);
        }

        //private readonly IBaseContextWrapper<ICar> _context;
        public async Task<ICar> AddCar(int idUser, string model)
        {
            var req = new AddCarRequest()
            {
                IdUser = idUser,
                Model = model
            };
            var res = await _client.AddCarAsync(req);
            return new Car_DAL()
            {
                Id = res.Id,
                Model = res.Model,
                IdUser = res.Id
            };
        }

        public async Task<ICar> DeleteCar(int id = 1)
        {
            var req = new DeleteCarRequest()
            {
                Id = id,
            };
            var res = await _client.DeleteCarAsync(req);
            return new Car_DAL()
            {
                Id = res.Id,
                Model = res.Model,
                IdUser = res.Id
            };
        }

        public async Task<ICar> GetCarById(int id)
        {
            var req = new GetCarByIdRequest()
            {
                Id = id,
            };
            var res = await _client.GetCarByIdAsync(req);
            return new Car_DAL()
            {
                Id = res.Id,
                Model = res.Model,
                IdUser = res.Id
            };
        }

        public async Task<IEnumerable<ICar>> GetCarsByUserId(int idUser)
        {
            var req = new GetCarByUserIdRequest()
            {
                IdUser = idUser,
            };
            using var res = _client.GetCarsByUserId(req);

            var list = new List<ICar>();

            await foreach (var data in res.ResponseStream.ReadAllAsync())
            {
                var temp = new Car_DAL()
                {
                    Id = data.Id,
                    Model = data.Model,
                    IdUser = data.Id
                };

                list.Add(temp);
            }

            return list;
        }
    }
}
