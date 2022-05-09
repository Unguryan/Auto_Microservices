using Grpc.Net.Client;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Clients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly UserGRPCService.UserGRPCServiceClient _client;

        public UserServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5004");
            _client = new UserGRPCService.UserGRPCServiceClient(channel);
        }
    }
}
