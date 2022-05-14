using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;

namespace UI.ViewModels.Car
{
    public class AddCarViewModel : BaseViewModel
    {
        private readonly IUser _activeUser;
        private readonly ICarServiceClient _carService;

        private readonly IViewModelAggregator _viewModelAggregator;
        public AddCarViewModel(IServices services)
        {
            _activeUser = services.ActiveUser;
            _carService = services.CarServiceClient;
            AddCarCommand = new RelayCommand(() => AddCarAction(), (_) => 
            {
                return !string.IsNullOrEmpty(Model);
            });
            BackCommand = new RelayCommand(() => BackAction());
        }

        public string Model { get; set; }


        public ICommand AddCarCommand { get; }

        public ICommand BackCommand { get; }

        public event Action OnClose;

        public event Action<ICar> OnAdded;

        private void AddCarAction()
        {
            ICar model = null;
            AsyncRunner.RunAsync(async () => await _carService.AddCar(_activeUser.Id, Model),ref model);
            OnAdded.Invoke(model);
        }

        private void BackAction()
        {
            OnClose.Invoke();
        }
    }
}
