using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UI.Extra;
using UI.Extra.Commands.Common;
using UI.Interfaces;

namespace UI.ViewModels.Order
{
    public class OrderUserViewModel : BaseViewModel
    {
        private readonly IOrderServiceClient _orderService;

        private readonly ICarServiceClient _carService;

        private readonly ICarStationServiceClient _carStationService;

        private readonly IUser _activeUser;

        public OrderUserViewModel(IServices services)
        {
            _orderService = services.OrderServiceClient;
            _carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _activeUser = services.ActiveUser;

            Orders = new ObservableCollection<OrderDataGridCellViewModel>();

            Init();

            RefreshCommand = new RelayCommand(() => RefreshAction());
        }

        public ObservableCollection<OrderDataGridCellViewModel> Orders { get; }

        public ICommand RefreshCommand { get; }

        private void Init()
        {
            Orders.Clear();

            IEnumerable<IOrder> orders = null;
            AsyncRunner.RunAsync(async () => await _orderService.GetOrdersByUserId(_activeUser.Id), ref orders);

            if(orders == null || !orders.Any())
            {
                return;
            }

            IEnumerable<ICar> cars = null;
            AsyncRunner.RunAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), ref cars);

            IEnumerable<ICarStation> carStations = null;
            AsyncRunner.RunAsync(async () => await _carStationService.GetCarStations(), ref carStations);

            if(cars == null || !cars.Any() && carStations == null || !carStations.Any())
            {
                return;
            }

            var list = new List<OrderDataGridCellViewModel>();

            foreach (var order in orders)
            {
                var temp = new OrderDataGridCellViewModel()
                {
                    Id = order.Id,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt.ToString()
                };

                temp.CarName = cars.FirstOrDefault(c => c.Id == order.IdCar)?.Model ?? string.Empty;
                temp.CarStationName = carStations.FirstOrDefault(c => c.Id == order.IdStation)?.Name ?? string.Empty;

                temp.Closed = order.Closed != DateTime.MinValue ? order.Closed.ToString() : string.Empty;

                temp.CompletedWork = new Dictionary<string, int>();

                foreach (var item in order.CompletedWork)
                {
                    temp.CompletedWork.Add(((WorkType)item.Key).ToString(), item.Value);
                }

                list.Add(temp);
            }

            list = list.OrderBy(o => o.Closed).ToList();

            list.ForEach(l => Orders.Add(l));
        }

        private void RefreshAction()
        {
            Init();
        }
    }
}
