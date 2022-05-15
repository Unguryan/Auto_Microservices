using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;
using UI.ViewModels.CarStation;
using UI.ViewModels.User;

namespace UI.ViewModels.Car
{
    public class RepairCarViewModel : BaseViewModel
    {
        private readonly ICarServiceClient _carService;

        private readonly ICarStationServiceClient _carStationService;

        private readonly IUser _activeUser;

        private readonly IViewModelAggregator _viewModelAggregator;

        private CarStationDataGridCellViewModel _selectedType;

        private ICarStation selectedCarStation;

        public RepairCarViewModel(IServices services)
        {
            _carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _activeUser = services.ActiveUser;
            _viewModelAggregator = services.ViewModelAggregator;

            TypesOfWork = new ObservableCollection<CarStationDataGridCellViewModel>();
            DataGridTypes = new ObservableCollection<CarStationDataGridCellViewModel>();
            Cars = new ObservableCollection<ICar>();
            CarStations = new ObservableCollection<ICarStation>();

            Init();

            AddTypeCommand = new RelayCommand(() => AddTypeAction(), (_) =>
             {
                 return TypesOfWork.Any() && SelectedType != null;
             });
            SendToCarStationCommand = new RelayCommand(() => SendToCarStationAction(), (_) =>
            {
                return SelectedCar != null
                    && SelectedCarStation != null
                    && DataGridTypes.Any();
            });
            BackCommand = new RelayCommand(() => BackAction());
        }

        public ObservableCollection<ICar> Cars { get; }

        public ICar SelectedCar { get; set; }

        public ObservableCollection<ICarStation> CarStations { get; }

        public ICarStation SelectedCarStation 
        { 
            get => selectedCarStation;
            set
            {
                selectedCarStation = value;
                OnPropertyChanged(nameof(SelectedCarStation));
                UpdateTypesOfWork();
            }
        }

       
        public ObservableCollection<CarStationDataGridCellViewModel> TypesOfWork { get; }

        public CarStationDataGridCellViewModel SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public ObservableCollection<CarStationDataGridCellViewModel> DataGridTypes { get; }

        public ICommand AddTypeCommand { get; }

        public ICommand SendToCarStationCommand { get; }

        public ICommand BackCommand { get; }

        //public event Action<IOrder> OnWentToCarStation;

        //public event Action OnClose;

        private void Init()
        {
            IEnumerable<ICar> cars = null;
            AsyncRunner.RunAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), ref cars);
            foreach (var item in cars)
            {
                Cars.Add(item);
            }

            IEnumerable<ICarStation> carStations = null;
            AsyncRunner.RunAsync(async () => await _carStationService.GetCarStations(), ref carStations);
            foreach (var item in carStations)
            {
                CarStations.Add(item);
            }
        }

        private void UpdateTypesOfWork()
        {
            TypesOfWork.Clear();
            SelectedType = null;
            DataGridTypes.Clear();

            foreach(var item in SelectedCarStation.TypeOfWork)
            {
                var temp = new CarStationDataGridCellViewModel() 
                {
                    Name = item.Key.ToString(),
                    Price = item.Value.ToString()
                };

                temp.RemoveCellEvent += OnRemoveCellEvent;

                TypesOfWork.Add(temp);
            }
        }

        private void OnRemoveCellEvent(CarStationDataGridCellViewModel obj)
        {
            DataGridTypes.Remove(obj);
            TypesOfWork.Add(obj);
            obj.RemoveCellEvent -= OnRemoveCellEvent;
        }

        private void BackAction()
        {
            //OnClose.Invoke();

            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }

        private void SendToCarStationAction()
        {
            var types = new Dictionary<int, int>();
            foreach (var item in DataGridTypes)
            {
                types.Add((int)Enum.Parse(typeof(WorkType), item.Name), int.Parse(item.Price));
            }

            var name = $"{_activeUser.Name} - {SelectedCar.Model}";

            IOrder order = null;
            AsyncRunner.RunAsync(async () => await _carStationService
            .StartWork(name, _activeUser.Id, SelectedCarStation.Id, SelectedCar.Id, types),
            ref order);

            //OnWentToCarStation.Invoke(order);
            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }

        private void AddTypeAction()
        {
            DataGridTypes.Add(SelectedType);
            TypesOfWork.Remove(SelectedType);
        }
    }
}
