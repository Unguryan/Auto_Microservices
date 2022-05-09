using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IClientFactory
    {
        IOrderServiceClient GetOrderServiceClient();

        ICarServiceClient GetCarServiceClient();

        ICarStationServiceClient GetCarStationServiceClient();

        IUserServiceClient GetUserServiceClient();
    }
}
