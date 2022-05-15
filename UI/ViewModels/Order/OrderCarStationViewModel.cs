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
using UI.ViewModels.User;

namespace UI.ViewModels.Order
{
    public class OrderCarStationViewModel : BaseViewModel
    {
        private readonly IOrderServiceClient _orderService;

        private readonly ICarServiceClient _carService;

        private readonly IUserServiceClient _userService;

        private readonly IUser _activeUser;

        private readonly ICarStation _activeCarStation;

        public OrderCarStationViewModel(IServices services)
        {
            _orderService = services.OrderServiceClient;
            _carService = services.CarServiceClient;
            _userService = services.UserServiceClient;
            _activeUser = services.ActiveUser;
            _activeCarStation = services.ActiveCarStation;

            OpenOrders = new ObservableCollection<OrderDataGridCellViewModel>();
            ClosedOrders = new ObservableCollection<OrderDataGridCellViewModel>();

            Init();

            RefreshCommand = new RelayCommand(() => RefreshAction());
            CloseOrderCommand = new RelayCommand(() => CloseOrderAction(), (_) =>
            {
                return SelectedOrder != null;
            });
        }

        

        public ObservableCollection<OrderDataGridCellViewModel> OpenOrders { get; }

        public OrderDataGridCellViewModel SelectedOrder { get; set; }

        public ObservableCollection<OrderDataGridCellViewModel> ClosedOrders { get; }

        public ICommand RefreshCommand { get; }

        public ICommand CloseOrderCommand { get; }


        private void Init()
        {
            OpenOrders.Clear();
            ClosedOrders.Clear();
            SelectedOrder = null;

            IEnumerable<IOrder> orders = null;
            AsyncRunner.RunAsync(async () => await _orderService.GetOrdersByOrderStationId(_activeCarStation.Id), ref orders);

            if (orders == null || !orders.Any())
            {
                return;
            }

            IEnumerable<IUser> users = null;
            AsyncRunner.RunAsync(async () => await _userService.GetUsers(), ref users);

            if (users == null || !users.Any())
            {
                return;
            }

            var list = new List<OrderDataGridCellViewModel>();

            foreach (var order in orders)
            {
                ICar car = null;
                AsyncRunner.RunAsync(async () => await _carService.GetCarById(order.IdCar), ref car);

                if(car == null)
                {
                    return;
                }

                var temp = new OrderDataGridCellViewModel()
                {
                    Id = order.Id,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt.ToString()
                };

                temp.CarName = car.Model;

                var userName = users.FirstOrDefault(c => c.Id == order.IdUser)?.Name ?? string.Empty;
                var phone = users.FirstOrDefault(c => c.Id == order.IdUser)?.Phone ?? string.Empty;
                temp.UserName = $"{userName} - {phone}";

                temp.Closed = order.Closed != DateTime.MinValue ? order.Closed.ToString() : string.Empty;

                temp.CompletedWork = new Dictionary<string, int>();

                foreach (var item in order.CompletedWork)
                {
                    temp.CompletedWork.Add(((WorkType)item.Key).ToString(), item.Value);
                }

                list.Add(temp);
            }

            //list = list.OrderBy(o => o.Closed).ToList();

            foreach(var item in list)
            {
                if(item.Closed == DateTime.MinValue.ToString() || string.IsNullOrEmpty(item.Closed))
                {
                    OpenOrders.Add(item);
                    continue;
                }

                ClosedOrders.Add(item);
            }
        }

        private void CloseOrderAction()
        {
            IOrder order = null;
            AsyncRunner.RunAsync(async () => await _orderService.CloseOrder(SelectedOrder.Id), ref order);
            
            Init();
        }

        private void RefreshAction()
        {
            Init();
        }
    }
}
