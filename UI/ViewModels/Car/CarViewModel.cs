using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;

namespace UI.ViewModels.Car
{
    public class CarViewModel : BaseViewModel
    {
        private readonly ICarServiceClient _carService;

        private readonly ICarStationServiceClient _carStationService;

        private readonly IViewModelAggregator _viewModelAggregator;

        private readonly IViewModelMapper _viewModelMapper;

        private readonly IUser _activeUser;

        private Visibility _addCarVisibility;

        public CarViewModel(IServices services)
        {
            _carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;
            _viewModelMapper = services.ViewModelMapper;
            _activeUser = services.ActiveUser;

            Cars = new ObservableCollection<ICar>();
            CarStations = new ObservableCollection<ICarStation>();

            Init();

            AddCarViewModel = _viewModelMapper.GetViewModelByType(typeof(AddCarViewModel));
            AddCarVisibility = Visibility.Collapsed;

            AddCarCommand = new RelayCommand(() => AddCarAction());
            RemoveCarCommand = new RelayCommand(() => RemoveCarAction());
            RepairCarCommand = new RelayCommand(() => RepairCarAction());
        }

        public ObservableCollection<ICar> Cars { get; }

        public ICar SelectedCar { get; set; }

        public ObservableCollection<ICarStation> CarStations { get; }

        public ICarStation SelectedCarStation { get; set; }

        public IViewModel AddCarViewModel { get; set; }

        public Visibility AddCarVisibility
        {
            get => _addCarVisibility;
            set
            {
                _addCarVisibility = value;
                OnPropertyChanged(nameof(AddCarVisibility));
            }
        }

        public ICommand AddCarCommand { get; }

        public ICommand RemoveCarCommand { get; }

        public ICommand RepairCarCommand { get; }

        private void Init()
        {
            IEnumerable<ICar> cars = null;
            AsyncRunner.RunAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), ref cars);

            if(cars != null)
            {
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }

            IEnumerable<ICarStation> carStations = null;
            AsyncRunner.RunAsync(async () => await _carStationService.GetCarStations(), ref carStations);

            if (carStations != null)
            {
                foreach (var carStation in carStations)
                {
                    CarStations.Add(carStation);
                }
            }
        }

        private void AddCarAction()
        {
            AddCarVisibility = Visibility.Visible;
            //TODO: Raise userControl
            AddCarViewModel = _viewModelMapper.GetViewModelByType(typeof(AddCarViewModel));
            var vm = AddCarViewModel as AddCarViewModel;
            vm.OnAdded += OnAdded;
            vm.OnClose += OnClose;
            //((AddCarViewModel)AddCarViewModel).OnAdded += OnAdded;
            //((AddCarViewModel)AddCarViewModel).OnClose += OnClose;
        }

        private void OnClose()
        {
            AddCarVisibility = Visibility.Collapsed;
        }

        private void OnAdded(ICar obj)
        {
            Cars.Add(obj);
            AddCarVisibility = Visibility.Collapsed;
        }

        private void RepairCarAction()
        {
            //TODO: Add OrderViewModel
        }

        private void RemoveCarAction()
        {
            ICar car = null;
            AsyncRunner.RunAsync(async () => await _carService.DeleteCar(SelectedCar.Id), ref car);

            Cars.Remove(SelectedCar);
        }
    }
}
