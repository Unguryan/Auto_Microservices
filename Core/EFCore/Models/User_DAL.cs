using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EFCore.Models
{
    public class User_DAL : IUser
    {
        public User_DAL()
        {
        }

        public User_DAL(int id, IUser source)
        {
            Id = id;
            Name = source.Name;
            Phone = source.Phone;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public void OrderCompleted(IOrder action)
        {
        }
    }
}
