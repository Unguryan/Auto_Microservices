using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class CarStationUI : ICarStation
    {
        public CarStationUI(int id, ICarStation source)
        {
            Id = id;
            IdOwner = source.IdOwner;
            Name = source.Name;
            TypeOfWork = source.TypeOfWork;
        }

        public CarStationUI(int id, int idOwner, string name, IDictionary<WorkType, int> typeOfWork)
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
