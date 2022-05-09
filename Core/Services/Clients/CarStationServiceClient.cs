using Grpc.Net.Client;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
