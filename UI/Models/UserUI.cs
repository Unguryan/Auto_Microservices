using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class UserUI : IUser
    {
        public UserUI()
        {
        }

        public UserUI(int id, IUser source)
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
