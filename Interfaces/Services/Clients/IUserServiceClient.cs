using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services.Clients
{
    public interface IUserServiceClient : IClient<IUser>
    {
        Task<IUser> AuthUser(string userName, string password);
        Task<IUser> RegUser(string userName, string password, string name, string phone);
        Task<IUser> GetUserById(int id);
        Task<IEnumerable<IUser>> GetUsers();
        Task<IUser> DeleteUser(int id);
        Task NotifyUser(int userId, int oderId);
    }
}
