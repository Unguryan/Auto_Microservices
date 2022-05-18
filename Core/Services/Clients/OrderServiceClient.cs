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
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly OrderGRPCService.OrderGRPCServiceClient _client;

        public OrderServiceClient()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5003", new GrpcChannelOptions { HttpHandler = httpHandler });

            //var channel = GrpcChannel.ForAddress("https://localhost:5003");
            _client = new OrderGRPCService.OrderGRPCServiceClient(channel);
        }

        public async Task<IOrder> AddOrder(string name,
                                           int idStation, 
                                           int idUser, 
                                           int idCar,
                                           string createdAt, 
                                           IDictionary<int, int> completedWork)
        {
            Thread.Sleep(1000);
            var req = new AddOrderRequest()
            {
                Name = name,
                IdStation = idStation,
                IdUser = idUser,
                IdCar = idCar,
                CreatedAt = createdAt
            };
            req.CompletedWork.Add(completedWork);

            var res = await _client.AddOrderAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                IdCar = res.IdCar,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };

        }

        public async Task<IOrder> CloseOrder(int id)
        {
            Thread.Sleep(1000);
            var req = new CloseOrderRequest()
            {
                Id = id
            };
            var res = await _client.CloseOrderAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                IdCar = res.IdCar,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };
        }

        public async Task<IOrder> DeleteOrder(int id)
        {
            Thread.Sleep(1000);
            var req = new DeleteOrderRequest()
            {
                Id = id
            };
            var res = await _client.DeleteOrderAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                IdCar = res.IdCar,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };
        }

        public async Task<IOrder> GetOrderById(int id)
        {
            Thread.Sleep(1000);
            var req = new GetOrderByIdRequest()
            {
                Id = id
            };
            var res = await _client.GetOrderByIdAsync(req);
            return new Order_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                IdStation = res.IdStation,
                IdUser = res.IdUser,
                IdCar = res.IdCar,
                CreatedAt = DateTime.Parse(res.CreatedAt),
                Closed = DateTime.Parse(res.Closed),
                CompletedWork = res.CompletedWork
            };
        }

        public async Task<IEnumerable<IOrder>> GetOrders()
        {
            Thread.Sleep(1000);
            var req = new GetOrdersRequest()
            {
            };
            using var res = _client.GetOrders(req);

            var list = new List<IOrder>();

            while (await res.ResponseStream.MoveNext())
            {
                var current = res.ResponseStream.Current;
                var temp = new Order_DAL()
                {
                    Id = current.Id,
                    Name = current.Name,
                    IdStation = current.IdStation,
                    IdUser = current.IdUser,
                    IdCar = current.IdCar,
                    CreatedAt = DateTime.Parse(current.CreatedAt),
                    Closed = DateTime.Parse(current.Closed),
                    CompletedWork = current.CompletedWork
                };
                list.Add(temp);
            }

            res.Dispose();
            return list;
        }

        public async Task<IEnumerable<IOrder>> GetOrdersByCarId(int id)
        {
            Thread.Sleep(1000);
            var req = new GetOrderByIdRequest()
            {
                Id = id
            };
            var list = new List<IOrder>();

            using var res = _client.GetOrdersByCarId(req);

            while (await res.ResponseStream.MoveNext())
            {
                var current = res.ResponseStream.Current;
                var temp = new Order_DAL()
                {
                    Id = current.Id,
                    Name = current.Name,
                    IdStation = current.IdStation,
                    IdUser = current.IdUser,
                    IdCar = current.IdCar,
                    CreatedAt = DateTime.Parse(current.CreatedAt),
                    Closed = DateTime.Parse(current.Closed),
                    CompletedWork = current.CompletedWork
                };
                list.Add(temp);
            }
            res.Dispose();
            return list;
        }

        public async Task<IEnumerable<IOrder>> GetOrdersByOrderStationId(int id)
        {
            Thread.Sleep(1000);
            var req = new GetOrderByIdRequest()
            {
                Id = id
            };
            var list = new List<IOrder>();

            using var res = _client.GetOrdersByOrderStationId(req);
            while (await res.ResponseStream.MoveNext())
            {
                var current = res.ResponseStream.Current;
                var temp = new Order_DAL()
                {
                    Id = current.Id,
                    Name = current.Name,
                    IdStation = current.IdStation,
                    IdUser = current.IdUser,
                    IdCar = current.IdCar,
                    CreatedAt = DateTime.Parse(current.CreatedAt),
                    Closed = DateTime.Parse(current.Closed),
                    CompletedWork = current.CompletedWork
                };
                list.Add(temp);
            }

            //using var res = _client.GetOrdersByOrderStationId(req);
            //await foreach (var data in res.ResponseStream.ReadAllAsync())
            //{
            //    var temp = new Order_DAL()
            //    {
            //        Id = data.Id,
            //        Name = data.Name,
            //        IdStation = data.IdStation,
            //        IdUser = data.IdUser,
            //        IdCar = data.IdCar,
            //        CreatedAt = DateTime.Parse(data.CreatedAt),
            //        Closed = DateTime.Parse(data.Closed),
            //        CompletedWork = data.CompletedWork
            //    };
            //    list.Add(temp);
            //}

            res.Dispose();
            return list;
        }

        public async Task<IEnumerable<IOrder>> GetOrdersByUserId(int id)
        {
            //Async does not work
            Thread.Sleep(1000);

            var req = new GetOrderByIdRequest()
            {
                Id = id
            };
            var list = new List<IOrder>();

            using var res = _client.GetOrdersByUserId(req);

            while (await res.ResponseStream.MoveNext())
            {
                var current = res.ResponseStream.Current;
                var temp = new Order_DAL()
                {
                    Id = current.Id,
                    Name = current.Name,
                    IdStation = current.IdStation,
                    IdUser = current.IdUser,
                    IdCar = current.IdCar,
                    CreatedAt = DateTime.Parse(current.CreatedAt),
                    Closed = DateTime.Parse(current.Closed),
                    CompletedWork = current.CompletedWork
                };
                list.Add(temp);
            }

            res.Dispose();
            return list;
        }
    }
}
