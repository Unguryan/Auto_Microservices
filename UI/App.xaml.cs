using Core.Services.Clients;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI.View;
using UI.ViewModels;
using UI.ViewModels.User;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginView view = new LoginView();
            LoginViewModel viewModel = new LoginViewModel(new UserServiceClient(), view.Close);
            view.DataContext = viewModel;

            if(!viewModel.IsAuth)
                view.Show();
        }
    }
}
