using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class UserUI : IUser
    {
        public UserUI(int id, IUser source)
        {
            Id = id;
            Name = source.Name;
            Phone = source.Phone;
        }

        public UserUI(int id, string name, string phone)
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
        }
    }
}
