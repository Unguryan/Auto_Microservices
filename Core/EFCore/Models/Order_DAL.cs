using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.EFCore.Models
{
    public class Order_DAL : IOrder
    {
        public Order_DAL()
        {
        }

        public Order_DAL(IOrder source)
        {
            Name = source.Name;
            IdStation = source.IdStation;
            IdUser = source.IdUser;
            CreatedAt = source.CreatedAt;
            Closed = source.Closed;
            CompletedWork = source.CompletedWork;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int IdStation { get; set; }

        public int IdUser { get; set; }

        public int IdCar { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Closed { get; set; }

        public IDictionary<int, int> CompletedWork { get; set; }
        
        public bool IsClosed { get; }
    }
}
