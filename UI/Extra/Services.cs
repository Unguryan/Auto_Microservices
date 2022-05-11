using Core.Services;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Interfaces;
using Unity;

namespace UI.Extra
{
    public class Services : IServices
    {
        private readonly ClientFactory _clientFactory;

        public Services()
        {
            _clientFactory = new ClientFactory();
            ViewModelMapper = new ViewModelMapper(this);
            ViewModelAggregator = new ViewModelAggregator();

            UIModule.RegisterVMs(ViewModelMapper);

            //Resolver = new UnityContainer();
            //UIModule.RegisterModules(Resolver);
        }

        public ICarServiceClient CarServiceClient => _clientFactory.GetCarServiceClient();

        public ICarStationServiceClient CarStationServiceClient => _clientFactory.GetCarStationServiceClient();

        public IOrderServiceClient OrderServiceClient => _clientFactory.GetOrderServiceClient();

        public IUserServiceClient UserServiceClient => _clientFactory.GetUserServiceClient();

        public IViewModelMapper ViewModelMapper { get; }

        public IViewModelAggregator ViewModelAggregator { get; }


        //public IUnityContainer Resolver { get; }
    }
}
