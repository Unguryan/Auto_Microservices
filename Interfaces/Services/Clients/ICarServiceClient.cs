using Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Services.Clients
{
    public interface ICarServiceClient : IClient<ICar>
    {
        Task<ICar> AddCar(int idUser, string model);

        Task<ICar> GetCarById(int id);

        Task<IEnumerable<ICar>> GetCarsByUserId(int idUser);

        Task<ICar> DeleteCar(int id = 1);
    }
}
