using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services.Clients
{
    public interface IOrderServiceClient : IClient<IOrder>
    {
        Task<IOrder> AddOrder(string name, int idStation, int idUser, int idCar, string createdAt, IDictionary<int, int> completedWork);
        Task<IOrder> GetOrderById(int id);
        Task<IEnumerable<IOrder>> GetOrders();
        Task<IEnumerable<IOrder>> GetOrdersByUserId(int id);
        Task<IEnumerable<IOrder>> GetOrdersByOrderStationId(int id);
        Task<IEnumerable<IOrder>> GetOrdersByCarId(int id);
        Task<IOrder> DeleteOrder(int id);
        Task<IOrder> CloseOrder(int id);
    }
}
