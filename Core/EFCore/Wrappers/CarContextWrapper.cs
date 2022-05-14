using Core.EFCore.Models;
using Interfaces.Models;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EFCore.Wrappers
{
    public class CarContextWrapper : IBaseContextWrapper<ICar>
    {
        private readonly AppContext _context;

        public CarContextWrapper(AppContext context)
        {
            _context = context;
            //if (!_CarContext.Cars.Any())
            //{
            //    _CarContext.Cars.Add(new Car_DAL(0, "Alex"));
            //    _CarContext.Cars.Add(new Car_DAL(1, "Dmytro"));
            //    _CarContext.Cars.Add(new Car_DAL(2, "Ihor"));
            //    _CarContext.SaveChanges();
            //}
        }

        public async Task<ICar> Add(ICar item)
        {
            //int id = 0;
            //if (_context.Cars.Any())
            //{
            //    id = _context.Cars.Select(car => car.Id).Max() + 1;
            //}

            var newItem = new Car_DAL(item);
            _context.Cars.Add(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }

        public async Task<IEnumerable<ICar>> Get()
        {
            return await Task.Run(() => 
            {
                return _context.Cars;
            });
        }

        public async Task<ICar> GetById(int id)
        {
            return await Task.Run(() =>
            {
                return _context.Cars.FirstOrDefault(car => car.Id == id);
            });
        }

        public async Task<bool> Put(int id, ICar item)
        {
            return await Task.Run( async () =>
            {
                var oldItem = _context.Cars.FirstOrDefault(car => car.Id == id);
                if (oldItem == null)
                {
                    return false;
                }

                _context.Cars.Remove(oldItem);

                var newItem = new Car_DAL(item);
                await _context.Cars.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return true;
            });
        }

        public async Task<ICar> Remove(int id)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.Cars.FirstOrDefault(car => car.Id == id);
                if (oldItem == null)
                {
                    return null;
                }

                _context.Cars.Remove(oldItem);
                await _context.SaveChangesAsync();
                return oldItem;
            });
        }
    }
}
