using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Models
{
    public interface ICarStation
    {
        int Id { get; } 

        int IdOwner { get; }

        string Name { get; }

        IDictionary<WorkType, int> TypeOfWork { get; }
    }
}
