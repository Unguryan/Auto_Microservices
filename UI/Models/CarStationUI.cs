using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class CarStationUI : ICarStation
    {
        public CarStationUI()
        {
        }

        public CarStationUI(int id, ICarStation source)
        {
            Id = id;
            IdOwner = source.IdOwner;
            Name = source.Name;
            TypeOfWork = source.TypeOfWork;
        }

        public int Id { get; set; }

        public int IdOwner { get; set; }

        public string Name { get; set; }

        public IDictionary<WorkType, int> TypeOfWork { get; set; }
    }
}
