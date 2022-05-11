using System;
using System.Collections.Generic;
using System.Text;
using UI.Interfaces;

namespace UI.Extra
{
    public class ViewModelAggregator : IViewModelAggregator
    {
        public event Action<Type> OnViewModelChanged;

        public void ChangeActiveVM(Type viewModel)
        {
            OnViewModelChanged?.Invoke(viewModel);
        }
    }
}
