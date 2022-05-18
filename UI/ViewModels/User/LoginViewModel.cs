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
using UI.Interfaces;
using UI.Models;
using UI.View;
using UI.View.User;
using UI.ViewModels.User;

namespace UI.ViewModels.User
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserServiceClient _userService;

        private readonly IViewModelAggregator _viewModelAggregator;

        

        private PasswordBox _passwordControl;

        public LoginViewModel(IServices services)
        {
            _userService = services.UserServiceClient;
            _viewModelAggregator = services.ViewModelAggregator;

            _viewModelAggregator.OnGetLastSavedUser += OnGetLastSavedUser;

            //GetLastSavedUser();


            LoginCommand = new RelayCommand<object>((x) => LoginAction(x));
            RegCommand = new RelayCommand(() => RegisterAction());
            ExitCommand = new RelayCommand(() => ExitAction());
        }

        private void OnGetLastSavedUser()
        {
            GetLastSavedUser();
        }

        public LoginViewModel()
        {
            
        }

        public string Username { get; set; }

        //public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public ICommand RegCommand { get; }

        public ICommand ExitCommand { get; }

        private void LoginAction(object o)
        {
            var passControl = o as PasswordBox;
            var password = passControl.Password;
            _passwordControl = passControl;

            //_userService.AuthUser(Username, password).RunAsync();
            //Task.Run(async () => user = await _userService.AuthUser(Username, password)).Wait();

            //IUser user = null;
            //AsyncRunner.RunAsync(async () => await _userService.AuthUser(Username, password), ref user);

            AsyncRunner.RunAsync(async () => await _userService.AuthUser(Username, password), CallBackAuthUser);

            //MessageBox.Show("MAIN!");
        }

        //private void Pass(Func<Task<IUser>> p)
        //{
        //    throw new NotImplementedException();
        //}

        //private void Pass(Task<IUser> task)
        //{
        //    throw new NotImplementedException();
        //}

        //private void Pass(Func<string, string, Task<IUser>> authUser)
        //{
        //    authUser.Invo
        //}

        private void RegisterAction()
        {
            //RegisterView view = new RegisterView();
            //RegisterViewModel viewModel = new RegisterViewModel(_userService, view.Close);
            //view.DataContext = viewModel;

            //view.Show();
            ////_closeWindow.Invoke();
            ///

            _viewModelAggregator.ChangeActiveVM(typeof(RegisterViewModel));
        }

        private void ExitAction()
        {

        }

        //private void GetLastSavedUser()
        //{
        //    var userAuth = LoginUtilities.GetSavedUser();
        //    if(userAuth == null)
        //    {
        //        return;
        //    }

        //    var res = MessageBox.Show("Log in with last saved user?", "Last session", MessageBoxButton.YesNo);

        //    if(res == MessageBoxResult.Yes)
        //    {
        //        IUser user = null;
        //        AsyncRunner.RunAsync(async () => await _userService.AuthUser(userAuth.UserName, userAuth.Password), ref user);

        //        if (user == null)
        //        {
        //            MessageBox.Show("Error");
        //            return;
        //        }

        //        //MainViewModel main = new MainViewModel();
        //        //MainView view = new MainView();
        //        //view.DataContext = main;
        //        //view.Show();

        //        //_closeWindow.Invoke();
        //        _viewModelAggregator.ChangeActiveUser(user);
        //        _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        //    }
        //    else
        //    {
        //        Username = string.Empty;
        //        if(_passwordControl != null)
        //            _passwordControl.Password = string.Empty;
        //        //Password = string.Empty;
        //    }
        //}

        private void GetLastSavedUser()
        {
            var userAuth = LoginUtilities.GetSavedUser();
            if (userAuth == null)
            {
                return;
            }

            var res = MessageBox.Show("Log in with last saved user?", "Last session", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                //IUser user = null;
                //AsyncRunner.RunAsync(async () => await _userService.AuthUser(userAuth.UserName, userAuth.Password), ref user);

                AsyncRunner.RunAsync(async () => await _userService.AuthUser(userAuth.UserName, userAuth.Password), CallBackAuthUser);


                //if (user == null)
                //{
                //    MessageBox.Show("Error");
                //    return;
                //}

                ////MainViewModel main = new MainViewModel();
                ////MainView view = new MainView();
                ////view.DataContext = main;
                ////view.Show();

                ////_closeWindow.Invoke();
                //_viewModelAggregator.ChangeActiveUser(user);
                //_viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
            }
            else
            {
                Username = string.Empty;
                if (_passwordControl != null)
                    _passwordControl.Password = string.Empty;
                //Password = string.Empty;
            }
        }

        private void CallBackAuthUser(IUser user)
        {
            if (user == null)
            {
                MessageBox.Show("Error");
            }


            if(!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(_passwordControl.Password))
            {
                var userAuth = LoginUtilities.GetSavedUser();

                if (userAuth == null || userAuth?.UserName != Username || userAuth?.Password != _passwordControl.Password)
                {
                    var res = MessageBox.Show("Save user?", "Save session", MessageBoxButton.YesNo);

                    if (res == MessageBoxResult.Yes)
                    {
                        LoginUtilities.SaveUser(new AuthUserUI(userAuth?.UserName, userAuth?.Password));
                        MessageBox.Show("Saved");
                    }
                }
            }

           

            _viewModelAggregator.ChangeActiveUser(user);
            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }
    }
}
