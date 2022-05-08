using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        string Phone { get; }

        public void OrderCompleted(IOrder action);
    }
}
