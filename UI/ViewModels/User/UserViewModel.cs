using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;
using UI.ViewModels.Car;
using UI.ViewModels.CarStation;
using UI.ViewModels.Order;

namespace UI.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IUser _activeUser;

        private readonly ICarStationServiceClient _carStationService;

        private readonly IViewModelAggregator _viewModelAggregator;

        private readonly IViewModelMapper _viewModelMapper;
        private Visibility _switchToCarStationVisibility;

        public UserViewModel(IServices services)
        {
            //_carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;
            _viewModelMapper = services.ViewModelMapper;
            _activeUser = services.ActiveUser;

            CarViewModel = _viewModelMapper.GetViewModelByType(typeof(CarViewModel));
            OrderUserViewModel = _viewModelMapper.GetViewModelByType(typeof(OrderUserViewModel));
            CarStations = new ObservableCollection<ICarStation>();

            Init();

            AddCarStationCommand = new RelayCommand(() => AddCarStationAction());
            SwitchToCarStationCommand = new RelayCommand(() => SwitchToCarStationAction());
        }

        public IViewModel CarViewModel { get; }

        public IViewModel OrderUserViewModel { get; }

        public string PreviewText => $"Welcome, {_activeUser.Name}";

        public Visibility SwitchToCarStationVisibility { 
            get => _switchToCarStationVisibility;
            set
            {
                _switchToCarStationVisibility = value;
                OnPropertyChanged(nameof(SwitchToCarStationVisibility));
            }
        }

        public ObservableCollection<ICarStation> CarStations { get; }

        public ICarStation SelectedCarStation { get; set; }

        public ICommand AddCarStationCommand { get; }

        public ICommand SwitchToCarStationCommand { get; }

        private void Init()
        {
            IEnumerable<ICarStation> carStations = null;
            AsyncRunner.RunAsync(async () => await _carStationService.GetCarStationByOwnerIdRequest(_activeUser.Id), ref carStations);

            if (carStations != null)
            {
                foreach (var carStation in carStations)
                {
                    CarStations.Add(carStation);
                }
            }

            SwitchToCarStationVisibility = CarStations.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SwitchToCarStationAction()
        {
            _viewModelAggregator.ChangeActiveCarStation(SelectedCarStation);
            _viewModelAggregator.ChangeActiveVM(typeof(CarStationViewModel));
        }

        private void AddCarStationAction()
        {
            _viewModelAggregator.ChangeActiveVM(typeof(AddCarStationViewModel));
        }

    }
}
