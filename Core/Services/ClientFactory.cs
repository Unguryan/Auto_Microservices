using Grpc.Net.Client;
using Interfaces.Services;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class ClientFactory : IClientFactory
    {
        public CarGRPCService.CarGRPCServiceClient GetCarServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            return new CarGRPCService.CarGRPCServiceClient(channel);
        }

        public CarStationGRPCService.CarStationGRPCServiceClient GetCarStationServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5002");
            return new CarStationGRPCService.CarStationGRPCServiceClient(channel);
        }

        public OrderGRPCService.OrderGRPCServiceClient GetOrderServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5003");
            return new OrderGRPCService.OrderGRPCServiceClient(channel);
        }

        public UserGRPCService.UserGRPCServiceClient GetCarUserServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5004");
            return new UserGRPCService.UserGRPCServiceClient(channel);
        }
    }
}
