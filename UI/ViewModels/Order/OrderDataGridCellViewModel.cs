using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UI.Extra.Commands.Common;

namespace UI.ViewModels.Order
{
    public class OrderDataGridCellViewModel
    {
        public OrderDataGridCellViewModel()
        {
            GetDetailsCommand = new RelayCommand(() => GetDetailsAction());
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string CarStationName { get; set; }

        public string UserName { get; set; }

        public string CarName { get; set; }

        public string CreatedAt { get; set; }

        public string Closed { get; set; }

        public IDictionary<string, int> CompletedWork { get; set; }

        public ICommand GetDetailsCommand { get; }

        //public bool IsClosed => Closed != DateTime.MinValue;

        private void GetDetailsAction()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{Name}");

            foreach (var item in CompletedWork)
            {
                sb.AppendLine($"{item.Key} - {item.Value} uah");
            }

            MessageBox.Show(sb.ToString());
        }
    }
}
