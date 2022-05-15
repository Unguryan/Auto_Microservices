using Core.Services;
using Interfaces.Models;
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

            ViewModelAggregator.OnChangingActiveUser += OnChangingActiveUser;
            ViewModelAggregator.OnChangingActiveCarStation += OnChangingActiveCarStation;

            //Resolver = new UnityContainer();
            //UIModule.RegisterModules(Resolver);
        }

        private void OnChangingActiveCarStation(ICarStation carStation)
        {
            ActiveCarStation = carStation;
        }

        private void OnChangingActiveUser(IUser user)
        {
            ActiveUser = user;
        }

        public ICarServiceClient CarServiceClient => _clientFactory.GetCarServiceClient();

        public ICarStationServiceClient CarStationServiceClient => _clientFactory.GetCarStationServiceClient();

        public IOrderServiceClient OrderServiceClient => _clientFactory.GetOrderServiceClient();

        public IUserServiceClient UserServiceClient => _clientFactory.GetUserServiceClient();

        public IViewModelMapper ViewModelMapper { get; }

        public IViewModelAggregator ViewModelAggregator { get; }

        public IUser ActiveUser { get; set; }

        public ICarStation ActiveCarStation { get; set; }


        //public IUnityContainer Resolver { get; }
    }
}
