using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UI.Extra.Commands.Common;

namespace UI.ViewModels.CarStation
{
    public class CarStationDataGridCellViewModel
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string FullName => $"{Name} - {Price}";

        public ICommand RemoveSelectedTypeCommand { get; }

        public event Action<CarStationDataGridCellViewModel> RemoveCellEvent;

        public CarStationDataGridCellViewModel()
        {
            RemoveSelectedTypeCommand = new RelayCommand(() => RemoveAction());
        }

        private void RemoveAction()
        {
            RemoveCellEvent.Invoke(this);
        }
    }
}
