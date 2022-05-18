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
        private readonly ICarServiceClient _carService;

        private readonly IUser _activeUser;

        public AddCarViewModel(IServices services)
        {
            _carService = services.CarServiceClient;
            _activeUser = services.ActiveUser;

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
            AsyncRunner.RunAsync(async () => await _carService.AddCar(_activeUser.Id, Model), AddCarCallBack);
        }

        private void AddCarCallBack(ICar model)
        {
            OnAdded.Invoke(model);
        }

        private void BackAction()
        {
            OnClose.Invoke();
        }
    }
}
