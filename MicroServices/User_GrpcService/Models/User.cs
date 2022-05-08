using Interfaces.Models;
using System;

namespace User_GrpcService.Models
{
    public class User : IUser
    {
        public User(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public User(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public int Id { get; }

        public string Name { get; }

        public string Phone { get; }

        public void OrderCompleted(IOrder action)
        {
            //TODO: Add Logic
        }
    }
}
