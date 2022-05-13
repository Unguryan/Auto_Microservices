using Core.Services.Clients;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;
using UI.View;
using UI.View.User;

namespace UI.ViewModels.User
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IUserServiceClient _userService;
        private readonly IViewModelAggregator _viewModelAggregator;

        //private Action _closeWindow;

        public RegisterViewModel(IServices services)
        {
            _userService = services.UserServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;
            //_closeWindow = closeWindowAction;

            RegCommand = new RelayCommand(() => RegisterAction());
            ExitCommand = new RelayCommand(() => ExitAction());
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public ICommand RegCommand { get; }

        public ICommand ExitCommand { get; }

        private void RegisterAction()
        {
            var res = _userService.RegUser(Username, Password, Name, Phone);

            res.Wait();

            if (res.Result == null)
            {
                MessageBox.Show("Username is already taken");
                return;
            }

            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
            //MessageBox.Show("Success");

            //LoginView view = new LoginView();
            //LoginViewModel viewModel = new LoginViewModel(_userService, );
            //view.DataContext = viewModel;

            //view.Show();

            //_closeWindow.Invoke();
        }

        private void ExitAction()
        {

        }
    }
}
