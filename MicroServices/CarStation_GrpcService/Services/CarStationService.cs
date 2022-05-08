using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Google.Protobuf.Collections;
using CarStation_GrpcService.Models;
using Grpc.Net.Client;
using System.Collections.Generic;
using System;

namespace CarStation_GrpcService.Services
{
    public class CarStationService : CarStationGRPCService.CarStationGRPCServiceBase
    {
        private readonly ILogger<CarStationService> _logger;

        private readonly IBaseContextWrapper<ICarStation> _context;

        private readonly IClientFactory _clientFactory;

        public CarStationService(ILogger<CarStationService> logger, IBaseContextWrapper<ICarStation> context, IClientFactory clientFactory)
        {
            _logger = logger;
            _context = context;
            _clientFactory = clientFactory;
        }

        public override async Task<CarStationModel> AddCarStation(AddCarStationRequest request, ServerCallContext context)
        {
            var typeOfWork = new Dictionary<WorkType, int>();
            foreach (var item in request.TypeOfWork)
            {
                typeOfWork.Add((WorkType)item.Key, item.Value);
            }
            var model = new CarStation(request.IdOwner, request.Name, typeOfWork);
            var temp = await _context.Add(model);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new CarStationModel()
            {
                Id = temp.Id,
                IdOwner = temp.IdOwner,
                Name = temp.Name,
            };

            foreach (var item in temp.TypeOfWork)
            {
                res.TypeOfWork.Add((int)item.Key, item.Value);
            }

            return res;
        }

        public override async Task<CarStationModel> DeleteCarStation(DeleteCarStationRequest request, ServerCallContext context)
        {
            var temp = await _context.Remove(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new CarStationModel()
            {
                Id = temp.Id,
                IdOwner = temp.IdOwner,
                Name = temp.Name,
            };

            foreach (var item in temp.TypeOfWork)
            {
                res.TypeOfWork.Add((int)item.Key, item.Value);
            }

            return res;
        }

        public override async Task<CarStationModel> GetCarStationById(GetCarStationByIdRequest request, ServerCallContext context)
        {
            var temp = await _context.GetById(request.Id);
            //Task.WaitAll();

            if (temp == null)
            {
                return null;
            }

            var res = new CarStationModel()
            {
                Id = temp.Id,
                IdOwner = temp.IdOwner,
                Name = temp.Name,
            };

            foreach (var item in temp.TypeOfWork)
            {
                res.TypeOfWork.Add((int)item.Key, item.Value);
            }

            return res;
        }

        public override async Task GetCarStationByOwnerId(GetCarStationByOwnerIdRequest request, IServerStreamWriter<CarStationModel> responseStream, ServerCallContext context)
        {
            foreach (var response in (await _context.Get()).Where(carStation => carStation.Id == request.Id))
            {
                var typeOfWork = new Dictionary<int, int>();
                foreach (var item in response.TypeOfWork)
                {
                    typeOfWork.Add((int)item.Key, item.Value);
                }
                var model = new CarStationModel()
                {
                    Id = response.Id,
                    IdOwner = response.IdOwner,
                    Name = response.Name,
                };

                model.TypeOfWork.Add(typeOfWork);

                await responseStream.WriteAsync(model);
            }
        }

        public override async Task GetCarStations(GetAllCarStationRequest request, IServerStreamWriter<CarStationModel> responseStream, ServerCallContext context)
        {
            foreach (var response in await _context.Get())
            {
                var typeOfWork = new Dictionary<int, int>();
                foreach (var item in response.TypeOfWork)
                {
                    typeOfWork.Add((int)item.Key, item.Value);
                }
                var model = new CarStationModel()
                {
                    Id = response.Id,
                    IdOwner = response.IdOwner,
                    Name = response.Name,
                };

                model.TypeOfWork.Add(typeOfWork);

                await responseStream.WriteAsync(model);
            }
        }

        public override async Task<Order> StartWork(StartWorkRequest request, ServerCallContext context)
        {
            var req = new AddOrderRequest()
            {
                Name = request.Name,
                IdUser = request.IdUser,
                IdStation = request.IdCarStation,
                CreatedAt = DateTime.Now.ToString()
            };
            //var station = await _context.GetById(request.IdCarStation);

            //var workType = station.TypeOfWork.Where((key, value) => request.TypeOfWork.Any(t => t == (int)key));
            //request.TypeOfWork
            req.CompletedWork.Add(request.TypeOfWork);

            //foreach (var item in workType)
            //{
            //}

            //req.CompletedWork.Add();

            var client = _clientFactory.GetOrderServiceClient();

            var res = await client.AddOrderAsync(req);

            var order = new Order()
            {
                Id = res.Id,
                Name = res.Name,
                IdUser = res.IdUser,
                IdStation = res.IdStation,
                CreatedAt = res.CreatedAt,
                Closed = res.Closed,
            };

            order.CompletedWork.Add(res.CompletedWork);

            return order;
        }

        public override async Task<Order> CloseWork(CloseWorkRequest request, ServerCallContext context)
        {
            var client = _clientFactory.GetOrderServiceClient();

            var req = new CloseOrderRequest()
            {
                Id = request.IdOrder
            };

            var res = await client.CloseOrderAsync(req);

            var order = new Order()
            {
                Id = res.Id,
                Name = res.Name,
                IdUser = res.IdUser,
                IdStation = res.IdStation,
                CreatedAt = res.CreatedAt,
                Closed = res.Closed,
            };

            order.CompletedWork.Add(res.CompletedWork);

            return order;
        }
    }
}
