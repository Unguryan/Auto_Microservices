using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EFCore.Models
{
    public class Car_DAL : ICar
    {
        public Car_DAL()
        {
        }

        public Car_DAL(int id, ICar source)
        {
            Id = id;
            IdUser = source.IdUser;
            Model = source.Model;
        }

        public int Id { get; set; }

        public int IdUser { get; set; }

        public string Model { get; set; }
    }
}
