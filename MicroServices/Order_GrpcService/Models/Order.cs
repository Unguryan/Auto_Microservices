using Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Order_GrpcService.Models
{
    public class Order : IOrder
    {
        public Order(string name, 
                    int idStation,
                    int idUser,
                    DateTime createdAt,
                    DateTime closed,
                    IDictionary<int, int> completedWork)
        {
            Name = name;
            IdStation = idStation;
            IdUser = idUser;
            CreatedAt = createdAt;
            Closed = closed;
            CompletedWork = completedWork;
        }

        public Order(int id, 
                    string name,
                    int idStation, 
                    int idUser,
                    DateTime createdAt, 
                    DateTime closed, 
                    IDictionary<int, int> completedWork)
        {
            Id = id;
            Name = name;
            IdStation = idStation;
            IdUser = idUser;
            CreatedAt = createdAt;
            Closed = closed;
            CompletedWork = completedWork;
        }

        public int Id { get; }

        public string Name { get; }

        public int IdStation { get; }

        public int IdUser { get; }

        public DateTime CreatedAt { get; }

        public DateTime Closed { get; }

        public IDictionary<int, int> CompletedWork { get; }
    }
}
