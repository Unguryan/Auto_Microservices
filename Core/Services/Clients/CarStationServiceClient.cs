using Core.EFCore.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Interfaces.Models;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services.Clients
{
    public class CarStationServiceClient : ICarStationServiceClient
    {
        private readonly CarStationGRPCService.CarStationGRPCServiceClient _client;

        public CarStationServiceClient()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5002", new GrpcChannelOptions { HttpHandler = httpHandler });

            //var channel = GrpcChannel.ForAddress("https://localhost:5002");
            _client = new CarStationGRPCService.CarStationGRPCServiceClient(channel);
        }

        public async Task<ICarStation> AddCarStation(int idOwner, string name, IDictionary<int, int> types)
        {
            Thread.Sleep(1000);
            var req = new AddCarStationRequest()
            {
                IdOwner = idOwner,              
                Name = name
            };
            foreach (var item in types)
            {
                req.TypeOfWork.Add(item.Key, item.Value);
            }

            var res = await _client.AddCarStationAsync(req);
            var returnValue = new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };

            foreach (var item in res.TypeOfWork)
            {
                returnValue.TypeOfWork.Add((WorkType)item.Key, item.Value);
            }

            return returnValue;
        }

        public async Task<IOrder> CloseWork(int idOrder)
        {
            Thread.Sleep(1000);
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
            Thread.Sleep(1000);
            var req = new DeleteCarStationRequest()
            {
                Id = id
            };
            var res = await _client.DeleteCarStationAsync(req);
            var returnValue = new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };

            foreach (var item in res.TypeOfWork)
            {
                returnValue.TypeOfWork.Add((WorkType)item.Key, item.Value);
            }

            return returnValue;
        }

        public async Task<ICarStation> GetCarStationById(int id)
        {
            Thread.Sleep(100);
            var req = new GetCarStationByIdRequest()
            {
                Id = id
            };
            var res = await _client.GetCarStationByIdAsync(req);
            var returnValue = new CarStation_DAL()
            {
                Id = res.Id,
                IdOwner = res.IdOwner,
                Name = res.Name
            };

            foreach (var item in res.TypeOfWork)
            {
                returnValue.TypeOfWork.Add((WorkType)item.Key, item.Value);
            }

            return returnValue;
        }

        public async Task<IEnumerable<ICarStation>> GetCarStationByOwnerIdRequest(int id)
        {
            Thread.Sleep(1000);
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

                foreach (var item in data.TypeOfWork)
                {
                    temp.TypeOfWork.Add((WorkType)item.Key, item.Value);
                }

                list.Add(temp);
            }

            res.Dispose();
            return list;
        }

        public async Task<IEnumerable<ICarStation>> GetCarStations()
        {
            Thread.Sleep(1000);
            //var req = new GetCarStationByIdRequest()
            //{
            //};
            //var res = await _client.GetCarStationByIdAsync(req);
            //return new CarStation_DAL()
            //{
            //    Id = res.Id,
            //    IdOwner = res.IdOwner,
            //    Name = res.Name
            //};

            var req = new GetAllCarStationRequest()
            {
            };

            using var res = _client.GetCarStations(req);

            var list = new List<ICarStation>();

            await foreach (var data in res.ResponseStream.ReadAllAsync())
            {
                var temp = new CarStation_DAL()
                {
                    Id = data.Id,
                    IdOwner = data.IdOwner,
                    Name = data.Name,
                    TypeOfWork = new Dictionary<WorkType, int>()
                };

                foreach (var item in data.TypeOfWork)
                {
                    temp.TypeOfWork.Add((WorkType)item.Key, item.Value);
                }

                list.Add(temp);
            }

            res.Dispose();
            return list;
        }

        public async Task<IOrder> StartWork(string name, int idUser, int idCarStation, int idCar, IDictionary<int, int> types)
        {
            Thread.Sleep(1000);
            var req = new StartWorkRequest()
            {
                Name = name,
                IdUser = idUser,
                IdCarStation = idCarStation,
                IdCar = idCar
            };

            foreach (var item in types)
            {
                req.TypeOfWork.Add(item.Key, item.Value);
            }

            var res = await _client.StartWorkAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdCar = res.IdCar,
                IdUser = res.IdUser,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };
        }
    }
}
