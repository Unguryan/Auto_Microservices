using Grpc.Net.Client;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Clients
{
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly OrderGRPCService.OrderGRPCServiceClient _client;

        public OrderServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5003");
            _client = new OrderGRPCService.OrderGRPCServiceClient(channel);
        }
    }
}
