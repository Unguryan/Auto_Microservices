using System;
using System.Collections.Generic;
using System.Text;
using UI.Interfaces;
using UI.View;
using UI.View.Car;
using UI.View.User;
using UI.ViewModels.Car;
using UI.ViewModels.User;
using Unity;

namespace UI.Extra
{
    public class UIModule
    {
        //public static void RegisterModules(IUnityContainer container)
        //{
            
        //}

        public static void RegisterVMs(IViewModelMapper mapper)
        {
            mapper.RegisterViewModel(typeof(LoginViewModel), typeof(LoginView));
            mapper.RegisterViewModel(typeof(RegisterViewModel), typeof(RegisterView));
            mapper.RegisterViewModel(typeof(UserViewModel), typeof(UserView));

            mapper.RegisterViewModel(typeof(CarViewModel), typeof(CarView));
            mapper.RegisterViewModel(typeof(AddCarViewModel), typeof(AddCarView));
        }
    }
}
