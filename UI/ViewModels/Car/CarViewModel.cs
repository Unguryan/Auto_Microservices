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

namespace UI.ViewModels.Car
{
    public class CarViewModel : BaseViewModel
    {
        private readonly ICarServiceClient _carService;

        private readonly ICarStationServiceClient _carStationService;

        private readonly IViewModelAggregator _viewModelAggregator;

        private readonly IViewModelMapper _viewModelMapper;

        private readonly IUser _activeUser;

        private readonly IDispatch _dispatch;

        private Visibility _addCarVisibility;

        private IViewModel _addCarViewModel;

        //private Visibility _repairCarVisibility;

        //private IViewModel _repairCarViewModel;

        //private bool _isRepairEnabled;

        public CarViewModel(IServices services)
        {
            _carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;
            _viewModelMapper = services.ViewModelMapper;
            _activeUser = services.ActiveUser;
            _dispatch = services.UIDispatcher;

            Cars = new ObservableCollection<ICar>();
            //CarStations = new ObservableCollection<ICarStation>();

            Init();

            var addVM = _viewModelMapper.GetViewModelByType(typeof(AddCarViewModel)) as AddCarViewModel;
            addVM.OnAdded += OnCarAdded;
            addVM.OnClose += OnCarAddViewClosed;
            AddCarViewModel = addVM;
            AddCarVisibility = Visibility.Collapsed;

            //var repairVM = _viewModelMapper.GetViewModelByType(typeof(RepairCarViewModel)) as RepairCarViewModel;
            //repairVM.OnWentToCarStation += OnWentToCarStation;
            //RepairCarViewModel = repairVM;

            //RepairCarVisibility = Visibility.Collapsed;

            AddCarCommand = new RelayCommand(() => AddCarAction());
            RemoveCarCommand = new RelayCommand(() => RemoveCarAction());
            RepairCarCommand = new RelayCommand(() => RepairCarAction(), (_) => { return Cars.Any(); });
        }

        

        public ObservableCollection<ICar> Cars { get; }

        public ICar SelectedCar { get; set; }

        //public ObservableCollection<ICarStation> CarStations { get; }

        //public ICarStation SelectedCarStation { get; set; }

        public Visibility AddCarVisibility
        {
            get => _addCarVisibility;
            set
            {
                _addCarVisibility = value;
                OnPropertyChanged(nameof(AddCarVisibility));
            }
        }

        public IViewModel AddCarViewModel 
        { 
            get => _addCarViewModel;
            set 
            {
                _addCarViewModel = value;
                OnPropertyChanged(nameof(AddCarViewModel));
            } 
        }

        //public Visibility RepairCarVisibility
        //{
        //    get => _repairCarVisibility;
        //    set
        //    {
        //        _repairCarVisibility = value;
        //        OnPropertyChanged(nameof(RepairCarVisibility));
        //    }
        //}

        //public IViewModel RepairCarViewModel
        //{
        //    get => _repairCarViewModel;
        //    set
        //    {
        //        _repairCarViewModel = value;
        //        OnPropertyChanged(nameof(RepairCarViewModel));
        //    }
        //}

        //public bool IsRepairEnabled { get => _isRepairEnabled;
        //    set 
        //    {
        //        _isRepairEnabled = value;
        //        OnPropertyChanged(nameof(IsRepairEnabled));
        //    } }

        public ICommand AddCarCommand { get; }

        public ICommand RemoveCarCommand { get; }

        public ICommand RepairCarCommand { get; }

        private void Init()
        {
            //IEnumerable<ICar> cars = null;
            AsyncRunner.RunAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), CallBackGetCars);

           

            //IEnumerable<ICarStation> carStations = null;
            //AsyncRunner.RunAsync(async () => await _carStationService.GetCarStations(), ref carStations);

            //if (carStations != null)
            //{
            //    foreach (var carStation in carStations)
            //    {
            //        CarStations.Add(carStation);
            //    }

            //    //IsRepairEnabled = ;
            //}
        }

        private void CallBackGetCars(IEnumerable<ICar> cars)
        {

            if (cars != null)
            {
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }

            //_dispatch.Invoke(() =>
            //{
            //    if (cars != null)
            //    {
            //        foreach (var car in cars)
            //        {
            //            Cars.Add(car);
            //        }
            //    }
            //});
        }

        private void AddCarAction()
        {
            AddCarVisibility = Visibility.Visible;
            //TODO: Raise userControl
            
            //var vm = AddCarViewModel as AddCarViewModel;
            //vm.OnAdded += OnAdded;
            //vm.OnClose += OnClose;
            //((AddCarViewModel)AddCarViewModel).OnAdded += OnAdded;
            //((AddCarViewModel)AddCarViewModel).OnClose += OnClose;
        }

        private void RepairCarAction()
        {
            _viewModelAggregator.ChangeActiveVM(typeof(RepairCarViewModel));
        }

        private void RemoveCarAction()
        {
            AsyncRunner.RunAsync(async () => await _carService.DeleteCar(SelectedCar.Id), CallBackDeleteCar);

            Cars.Remove(SelectedCar);
        }

        private void CallBackDeleteCar(ICar obj)
        {
            _dispatch.Invoke(() =>
            {
                MessageBox.Show($"Car was removed: {obj.Model}");
            });
        }

        private void OnCarAddViewClosed()
        {
            AddCarVisibility = Visibility.Collapsed;
        }

        private void OnCarAdded(ICar obj)
        {
            Cars.Add(obj);
            AddCarVisibility = Visibility.Collapsed;
        }
    }
}
