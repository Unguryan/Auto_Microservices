using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Models
{
    public interface ICar
    {
        public int Id { get; }

        public int IdUser { get; }

        public string Model { get; }
	}
}
