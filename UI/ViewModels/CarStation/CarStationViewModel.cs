using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;
using UI.ViewModels.Order;
using UI.ViewModels.User;

namespace UI.ViewModels.CarStation
{
    public class CarStationViewModel : BaseViewModel
    {
        private readonly IViewModelAggregator _viewModelAggregator;

        public CarStationViewModel(IServices services)
        {
            _viewModelAggregator = services.ViewModelAggregator;

            OrderCarStationViewModel = services.ViewModelMapper
                .GetViewModelByType(typeof(OrderCarStationViewModel));

            BackCommand = new RelayCommand(() => BackAction());
        }

        public IViewModel OrderCarStationViewModel { get; }

        public ICommand BackCommand { get; }

        private void BackAction()
        {
            _viewModelAggregator.ChangeActiveCarStation(null);
            _viewModelAggregator.ChangeActiveVM(typeof(UserViewModel));
        }
    }
}
