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
using UI.ViewModels.User;

namespace UI.ViewModels.CarStation
{
    public class AddCarStationViewModel : BaseViewModel
    {
        private readonly ICarStationServiceClient _carStationService;

        private readonly IViewModelAggregator _viewModelAggregator;

        private readonly IUser _activeUser;

        private string _selectedType;

        private int _price;
        

        public AddCarStationViewModel(IServices services)
        {
            _carStationService = services.CarStationServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;
            _activeUser = services.ActiveUser;

            TypesOfWork = new ObservableCollection<string>();
            DataGridTypes = new ObservableCollection<CarStationDataGridCellViewModel>();

            Init();

            AddTypeCommand = new RelayCommand(() => AddTypeAction(), (_) =>
            {
                return !string.IsNullOrEmpty(SelectedType) && Price > 0;
            });

            AddCarStationCommand = new RelayCommand(() => AddCarStationAction(), (_) =>
            {
                return !string.IsNullOrEmpty(Name) && DataGridTypes.Any();
            });


            BackCommand = new RelayCommand(() => BackAction());
        }

        public string Name { get; set; }

        public ObservableCollection<string> TypesOfWork { get; }

        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public int Price
        { 
            get => _price;
            set 
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ObservableCollection<CarStationDataGridCellViewModel> DataGridTypes { get; }

        //public CarStationDataGridCellViewModel SelectedDataGridType { get; set; }

        public ICommand AddTypeCommand { get; }

        public ICommand AddCarStationCommand { get; }

        public ICommand BackCommand { get; }

        private void Init()
        {
            var types = Enum.GetNames(typeof(WorkType));
            foreach (var type in types)
            {
                TypesOfWork.Add(type);
            }
        }

        private void BackAction()
        {
            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }

        private void AddCarStationAction()
        {
            var typeOfWork = new Dictionary<int, int>();

            foreach (var type in DataGridTypes)
            {
                var key = (WorkType)Enum.Parse(typeof(WorkType), type.Name);
                typeOfWork.Add((int)key, int.Parse(type.Price));
            }

            ICarStation carStation = null;
            AsyncRunner.RunAsync(async () => await _carStationService.AddCarStation(_activeUser.Id, Name, typeOfWork), ref carStation);

            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }

        private void AddTypeAction()
        {
            if (string.IsNullOrEmpty(SelectedType) && Price <= 0)
            {
                return;
            }


            var type = new CarStationDataGridCellViewModel()
            {
                Name = SelectedType,
                Price = Price.ToString()
            };
            type.RemoveCellEvent += OnRemoveCellEvent;
            DataGridTypes.Add(type);

            TypesOfWork.Remove(SelectedType);
            Price = 0;
            SelectedType = null;
        }

        private void OnRemoveCellEvent(CarStationDataGridCellViewModel obj)
        {
            TypesOfWork.Add(obj.Name);

            obj.RemoveCellEvent -= OnRemoveCellEvent;
            DataGridTypes.Remove(obj);
        }
    }
}
