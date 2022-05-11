using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class OrderUI : IOrder
    {
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

        public OrderUI(int id, 
                       string name, 
                       int idStation,
                       int idUser, 
                       int idCar, 
                       DateTime createdAt, 
                       DateTime closed, 
                       IDictionary<int, int> completedWork)
        {
            Id = id;
            Name = name;
            IdStation = idStation;
            IdUser = idUser;
            IdCar = idCar;
            CreatedAt = createdAt;
            Closed = closed;
            CompletedWork = completedWork;
        }

        public int Id { get; }

        public string Name { get; }

        public int IdStation { get; }

        public int IdUser { get; }

        public int IdCar { get; }

        public DateTime CreatedAt { get; }

        public DateTime Closed { get; set; }

        public IDictionary<int, int> CompletedWork { get; }
        
        public bool IsClosed => Closed != DateTime.MinValue;
    }
}
