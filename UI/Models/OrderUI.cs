using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class OrderUI : IOrder
    {
        public OrderUI()
        {
        }

        public OrderUI(int id, IOrder source)
        {
            Id = id;
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
