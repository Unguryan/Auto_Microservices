using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IBaseContextWrapper<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(T item);

        Task<T> Remove(int id);

        Task<T> Put(int id, T item);
    }
}
