using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class CarUI : ICar
    {
        public CarUI(int id, ICar source)
        {
            Id = id;
            IdUser = source.IdUser;
            Model = source.Model;
        }

        public CarUI(int id, int idUser, string model)
        {
            Id = id;
            IdUser = idUser;
            Model = model;
        }

        public int Id { get; }

        public int IdUser { get; }

        public string Model { get; }
    }
}
