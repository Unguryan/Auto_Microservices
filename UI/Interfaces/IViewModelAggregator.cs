using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Interfaces
{
    public interface IViewModelAggregator
    {
        event Action<Type> OnViewModelChanged;
        void ChangeActiveVM(Type viewModel);

        event Action OnGetLastSavedUser;
        void GetLastSavedUser();

        event Action<IUser> OnChangingActiveUser;
        void ChangeActiveUser(IUser user);

        event Action<ICarStation> OnChangingActiveCarStation;
        void ChangeActiveCarStation(ICarStation carStation);
    }
}
