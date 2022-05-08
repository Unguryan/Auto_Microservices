using Interfaces.Models;
using System.Collections.Generic;

namespace CarStation_GrpcService.Models
{
    public class CarStation : ICarStation
    {
        public CarStation(int idOwner, string name, IDictionary<WorkType, int> typeOfWork)
        {
            IdOwner = idOwner;
            Name = name;
            TypeOfWork = typeOfWork;
        }

        public CarStation(int id, int idOwner, string name, IDictionary<WorkType, int> typeOfWork)
        {
            Id = id;
            IdOwner = idOwner;
            Name = name;
            TypeOfWork = typeOfWork;
        }

        public int Id { get; }

        public int IdOwner { get; }

        public string Name { get; }

        public IDictionary<WorkType, int> TypeOfWork { get; }
    }
}
