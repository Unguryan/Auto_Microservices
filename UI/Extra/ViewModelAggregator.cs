using Interfaces.Models;
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

        public event Action OnGetLastSavedUser;

        public void GetLastSavedUser()
        {
            OnGetLastSavedUser?.Invoke();
        }

        public event Action<IUser> OnChangingActiveUser;

        public void ChangeActiveUser(IUser user)
        {
            OnChangingActiveUser?.Invoke(user);
        }

        public event Action<ICarStation> OnChangingActiveCarStation;

        public void ChangeActiveCarStation(ICarStation carStation)
        {
            OnChangingActiveCarStation?.Invoke(carStation);
        }
    }
}
