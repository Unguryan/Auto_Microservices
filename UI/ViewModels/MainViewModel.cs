using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using UI.Extra;
using UI.Interfaces;
using UI.ViewModels.User;

namespace UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private IViewModel _activeViewModel;

        private IServices _services;

        private UserControl activeView;

        public MainViewModel(IServices services)
        {
            _services = services;
            _services.ViewModelAggregator.OnViewModelChanged += OnViewModelChanged;
        }

        private IViewModel ActiveViewModel
        {
            get
            {
                return _activeViewModel;
            }
            set
            {
                //TODO Add Dispose for VM
                _activeViewModel = value;
                OnPropertyChanged(nameof(ActiveViewModel));

                _services.UIDispatcher.Invoke(() =>
                {
                    ActiveView = _services.ViewModelMapper.GetViewByViewModelType(_activeViewModel.GetType());
                });

                //ActiveView = _services.ViewModelMapper.GetViewByViewModelType(_activeViewModel.GetType());
                //RaisePropertyChanged
            }
        }

        public UserControl ActiveView
        {
            get 
            {
                return activeView;
            }
            private set 
            {
                activeView = value;
                activeView.DataContext = ActiveViewModel;
                OnPropertyChanged(nameof(ActiveView));
            }
        }

        private void OnViewModelChanged(Type obj)
        {
            ActiveViewModel = _services.ViewModelMapper.GetViewModelByType(obj);
        }
    }
}
