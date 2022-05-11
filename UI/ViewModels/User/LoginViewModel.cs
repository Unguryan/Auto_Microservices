using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Models;
using UI.View;
using UI.ViewModels.User;

namespace UI.ViewModels.User
{
    public class LoginViewModel
    {
        private readonly IUserServiceClient _userService;

        private Action _closeWindow;

        public LoginViewModel(IUserServiceClient userService, Action closeWindowAction)
        {
            _userService = userService;
            _closeWindow = closeWindowAction;

            IsAuth = false;

            GetLastSavedUser();


            LoginCommand = new RelayCommand<object>((x) => LoginAction(x));
            RegCommand = new RelayCommand(() => RegisterAction());
            ExitCommand = new RelayCommand(() => ExitAction());
        }

        public string Username { get; set; }

        //public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public ICommand RegCommand { get; }

        public ICommand ExitCommand { get; }
        public bool IsAuth { get; set; }

        private void LoginAction(object o)
        {
            var passControl = o as PasswordBox;

            var password = passControl.Password;
            IUser user = null;
            Task.Run(async () => user = await _userService.AuthUser(Username, password)).Wait();

            //user.Wait();
            if(user == null)
            {
                MessageBox.Show("Error");
            }

            var userAuth = LoginUtilities.GetSavedUser();
            if(userAuth == null || userAuth?.UserName != Username)
            {
                var res = MessageBox.Show("Save user?", "Save session", MessageBoxButton.YesNo);

                if (res == MessageBoxResult.Yes)
                {
                    LoginUtilities.SaveUser(new AuthUserUI(Username, password));
                    MessageBox.Show("Saved");
                }
            }

            MessageBox.Show("MAIN!");
        }

        private void RegisterAction()
        {
            RegisterView view = new RegisterView();
            RegisterViewModel viewModel = new RegisterViewModel(_userService, view.Close);
            view.DataContext = viewModel;

            view.Show();
            _closeWindow.Invoke();
        }

        private void ExitAction()
        {

        }

        private void GetLastSavedUser()
        {
            var userAuth = LoginUtilities.GetSavedUser();
            if(userAuth == null)
            {
                return;
            }

            var res = MessageBox.Show("Log in with last saved user?", "Last session", MessageBoxButton.YesNo);

            if(res == MessageBoxResult.Yes)
            {
                IUser user = null;
                Task.Run(async () => user = await _userService.AuthUser(userAuth.UserName, userAuth.Password)).Wait();
                
                if (user == null)
                {
                    MessageBox.Show("Error");
                    return;
                }

                MainViewModel main = new MainViewModel();
                MainView view = new MainView();
                view.DataContext = main;
                view.Show();

                IsAuth = true;

                _closeWindow.Invoke();
            }
            else
            {
                   Username = string.Empty;
                //Password = string.Empty;
            }
        }
    }
}
