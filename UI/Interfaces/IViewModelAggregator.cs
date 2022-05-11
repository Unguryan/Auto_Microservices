using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Interfaces
{
    public interface IViewModelAggregator
    {
        event Action<Type> OnViewModelChanged;

        void ChangeActiveVM(Type viewModel);
    }
}
