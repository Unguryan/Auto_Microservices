using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace UI.Interfaces
{
    public interface IServices
    {
        ICarServiceClient CarServiceClient { get; }

        ICarStationServiceClient CarStationServiceClient { get; }

        IOrderServiceClient OrderServiceClient { get; }

        IUserServiceClient UserServiceClient { get; }

        IViewModelMapper ViewModelMapper { get; }

        IViewModelAggregator ViewModelAggregator { get; }

        //IUnityContainer Resolver { get; }
    }
}
