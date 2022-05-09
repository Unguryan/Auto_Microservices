using Core.Services.Clients;
using Grpc.Net.Client;
using Interfaces.Services;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class ClientFactory : IClientFactory
    {
        public ICarServiceClient GetCarServiceClient()
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //return new CarGRPCService.CarGRPCServiceClient(channel);

            return new CarServiceClient();
        }

        public ICarStationServiceClient GetCarStationServiceClient()
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5002");
            //return new CarStationGRPCService.CarStationGRPCServiceClient(channel);

            return new CarStationServiceClient();
        }

        public IOrderServiceClient GetOrderServiceClient()
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5003");
            //return new OrderGRPCService.OrderGRPCServiceClient(channel);
            return new OrderServiceClient();
        }

        public IUserServiceClient GetUserServiceClient()
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5004");
            //return new UserGRPCService.UserGRPCServiceClient(channel);
            return new UserServiceClient();
        }
    }
}
