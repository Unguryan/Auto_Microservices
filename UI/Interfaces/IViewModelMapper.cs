using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using UI.ViewModels.User;

namespace UI.Interfaces
{
    public interface IViewModelMapper
    {
        void RegisterViewModel(Type viewModel, Type view);

        UserControl GetViewByViewModelType(Type viewModelType);

        IViewModel GetViewModelByType(Type viewModelType);
    }
}
