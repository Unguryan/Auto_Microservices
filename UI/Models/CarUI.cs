using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    public class CarUI : ICar
    {
        public CarUI()
        {
        }

        public CarUI(int id, ICar source)
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
