using Core.EFCore.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Interfaces.Models;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Clients
{
    public class CarStationServiceClient : ICarStationServiceClient
    {
        private readonly CarStationGRPCService.CarStationGRPCServiceClient _client;

        public CarStationServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5002");
            _client = new CarStationGRPCService.CarStationGRPCServiceClient(channel);
        }

        public async Task<ICarStation> AddCarStation(int idOwner, string name)
        {
            var req = new AddCarStationRequest()
            {
                IdOwner = idOwner,              
                Name = name
            };
            var res = await _client.AddCarStationAsync(req);
            return new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };
        }

        public async Task<IOrder> CloseWork(int idOrder)
        {
            var req = new CloseWorkRequest()
            {
                IdOrder = idOrder
            };
            var res = await _client.CloseWorkAsync(req);
            return new Order_DAL()
            { 
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
        };
        }

        public async Task<ICarStation> DeleteCarStation(int id)
        {
            var req = new DeleteCarStationRequest()
            {
                Id = id
            };
            var res = await _client.DeleteCarStationAsync(req);
            return new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };
        }

        public async Task<ICarStation> GetCarStationById(int id)
        {
            var req = new GetCarStationByIdRequest()
            {
                Id = id
            };
            var res = await _client.GetCarStationByIdAsync(req);
            return new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };
        }

        public async Task<IEnumerable<ICarStation>> GetCarStationByOwnerIdRequest(int id)
        {
            var req = new GetCarStationByOwnerIdRequest()
            {
                Id = id
            };
            using var res = _client.GetCarStationByOwnerId(req);

            var list = new List<ICarStation>();

            await foreach (var data in res.ResponseStream.ReadAllAsync())
            {
                var temp = new CarStation_DAL()
                {
                Id = data.Id,
                IdOwner = data.IdOwner,
                Name = data.Name
                };
                list.Add(temp);
            }
            return list;
        }

        public async Task<ICarStation> GetCarStations()
        {
            var req = new GetCarStationByIdRequest()
            {
            };
            var res = await _client.GetCarStationByIdAsync(req);
            return new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };
        }

        public async Task<IOrder> StartWork(string name, int idUser, int idCarStation, int idCar)
        {
            var req = new StartWorkRequest()
            {
                Name = name,
                IdUser = idUser,
                IdCarStation = idCarStation,
                IdCar = idCar
            };
            var res = await _client.StartWorkAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };
        }
    }
}
