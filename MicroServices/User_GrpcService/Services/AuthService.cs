using System.Collections.Generic;
using System.Linq;
using User_GrpcService.Models;

namespace User_GrpcService.Services
{
    public class AuthService
    {
        private readonly IList<AuthUser> _users;

        private readonly AuthSerializer _serializer;

        public AuthService()
        {
            _serializer = new AuthSerializer();
            _users = _serializer.Deserialize();
        }

        public bool CheckUsername(string userName)
        {
            return _users.Any(u => u.UserName == userName);
        }

        public void RegisterUser(int userId, string userName, string password)
        {
            if(_users.Any(u => u.Id == userId || u.UserName == userName))
            {
                return;
            }

            _users.Add(new AuthUser()
            {
                Id = userId,
                UserName = userName,
                Password = password
            });

            _serializer.Serialize(_users);
        }

        public int AuthUser(string userName, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user == null)
            {
                return -1;
            }

            return user.Id;
        }

        public bool RemoveUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            _users.Remove(user);
            _serializer.Serialize(_users);
            return true;
        }
    }
}
