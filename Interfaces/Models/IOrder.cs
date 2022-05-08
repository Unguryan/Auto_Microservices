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

        DateTime CreatedAt { get; }

        DateTime Closed { get; }

        IDictionary<int, int> CompletedWork { get; }
    }
}
