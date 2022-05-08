using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IClientFactory
    {
        OrderGRPCService.OrderGRPCServiceClient GetOrderServiceClient();

        CarGRPCService.CarGRPCServiceClient GetCarServiceClient();

        CarStationGRPCService.CarStationGRPCServiceClient GetCarStationServiceClient();

        UserGRPCService.UserGRPCServiceClient GetCarUserServiceClient();
    }
}
