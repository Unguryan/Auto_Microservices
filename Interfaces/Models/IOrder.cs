using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Models
{
    public interface IOrder
    {
        int Id { get; }

        string Name { get; }

        int IdStation { get; }

        int IdUser { get; }

        int IdCar { get; }

        DateTime CreatedAt { get; }

        DateTime Closed { get; set; }

        IDictionary<int, int> CompletedWork { get; }
    }
}
