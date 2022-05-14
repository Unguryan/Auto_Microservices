using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.EFCore.Models
{
    public class CarStation_DAL : ICarStation
    {
        public CarStation_DAL()
        {
        }

        public CarStation_DAL(ICarStation source)
        {
            IdOwner = source.IdOwner;
            Name = source.Name;
            TypeOfWork = source.TypeOfWork;
        }

        public int Id { get; set; }

        public int IdOwner { get; set; }

        public string Name { get; set; }

        public IDictionary<WorkType, int> TypeOfWork { get; set; } = new Dictionary<WorkType, int>();
    }
}
