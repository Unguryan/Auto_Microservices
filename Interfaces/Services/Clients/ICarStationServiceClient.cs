using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services.Clients
{
    public interface ICarStationServiceClient : IClient<ICarStation>
    {
        Task<ICarStation> AddCarStation(int idOwner, string name, IDictionary<int, int> types);
        Task<ICarStation> GetCarStationById(int id);
        Task<IEnumerable<ICarStation>> GetCarStationByOwnerIdRequest (int id);
        Task<IEnumerable<ICarStation>> GetCarStations();
        Task<ICarStation> DeleteCarStation(int id);
        Task<IOrder> StartWork(string name, int idUser, int idCarStation, int idCar, IDictionary<int, int> types);
        Task<IOrder> CloseWork(int idOrder);
    }
}
