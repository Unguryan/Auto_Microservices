using Interfaces.Models;
using Interfaces.Services.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IDispatch _dispatch;

        public OrderUserViewModel(IServices services)
        {
            _orderService = services.OrderServiceClient;
            _carService = services.CarServiceClient;
            _carStationService = services.CarStationServiceClient;
            _activeUser = services.ActiveUser;
            _dispatch = services.UIDispatcher;

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
            IEnumerable<ICar> cars = null;
            IEnumerable<ICarStation> carStations = null;

            AsyncRunner.RunTaskAsync(async () => await _orderService.GetOrdersByUserId(_activeUser.Id), (o) =>
            {
                orders = o;
            })
            .ContinueAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), (c) =>
            {
                cars = c;
            })
            .ContinueAsync(async () => await _carStationService.GetCarStations(), (ct) =>
            {
                carStations = ct;
            })
            .ContinueAsync(async () => await InitAsync(orders, cars, carStations));

            //AsyncRunner.RunAsync(async () => await _orderService.GetOrdersByUserId(_activeUser.Id), CallBackGetOrdersByUserId);

            //if(orders == null || !orders.Any())
            //{
            //    return;
            //}

            //AsyncRunner.RunAsync(async () => await _carService.GetCarsByUserId(_activeUser.Id), CallBackGetCarsByUserId);

            //AsyncRunner.RunAsync(async () => await _carStationService.GetCarStations(), CallBackGetCarStations);


            //if (orders == null || !orders.Any())
            //{
            //    return;
            //}

            //if (cars == null || !cars.Any() && carStations == null || !carStations.Any())
            //{
            //    return;
            //}

            //var list = new List<OrderDataGridCellViewModel>();

            //foreach (var order in orders)
            //{
            //    var temp = new OrderDataGridCellViewModel()
            //    {
            //        Id = order.Id,
            //        Name = order.Name,
            //        CreatedAt = order.CreatedAt.ToString()
            //    };

            //    temp.CarName = cars.FirstOrDefault(c => c.Id == order.IdCar)?.Model ?? string.Empty;
            //    temp.CarStationName = carStations.FirstOrDefault(c => c.Id == order.IdStation)?.Name ?? string.Empty;

            //    temp.Closed = order.Closed != DateTime.MinValue ? order.Closed.ToString() : string.Empty;

            //    temp.CompletedWork = new Dictionary<string, int>();

            //    foreach (var item in order.CompletedWork)
            //    {
            //        temp.CompletedWork.Add(((WorkType)item.Key).ToString(), item.Value);
            //    }

            //    list.Add(temp);
            //}

            //list = list.OrderBy(o => o.Closed).ToList();

            //list.ForEach(l => Orders.Add(l));
        }

        private async Task InitAsync(IEnumerable<IOrder> orders, IEnumerable<ICar> cars, IEnumerable<ICarStation> carStations)
        {

            if (orders == null || !orders.Any())
            {
                return;
            }

            //if (cars == null || !cars.Any() && carStations == null || !carStations.Any())
            //{
            //    return;
            //}

            var list = new List<OrderDataGridCellViewModel>();

            foreach (var order in orders)
            {
                var temp = new OrderDataGridCellViewModel()
                {
                    Id = order.Id,
                    Name = order.Name,
                    CreatedAt = order.CreatedAt.ToString()
                };

                temp.CarName = cars.FirstOrDefault(c => c.Id == order.IdCar)?.Model ?? "NOT FOUND";
                temp.CarStationName = carStations.FirstOrDefault(c => c.Id == order.IdStation)?.Name ?? "NOT FOUND";

                temp.Closed = order.Closed != DateTime.MinValue ? order.Closed.ToString() : string.Empty;

                temp.CompletedWork = new Dictionary<string, int>();

                foreach (var item in order.CompletedWork)
                {
                    temp.CompletedWork.Add(((WorkType)item.Key).ToString(), item.Value);
                }

                list.Add(temp);
            }

            list = list.OrderBy(o => o.Closed).ToList();


            _dispatch.Invoke(() =>
            {
                list.ForEach(l => Orders.Add(l));
            });
        }


        private void RefreshAction()
        {
            Init();
        }
    }
}
