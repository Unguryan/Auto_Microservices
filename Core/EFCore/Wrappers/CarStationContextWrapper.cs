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
    public class CarStationContextWrapper : IBaseContextWrapper<ICarStation>
    {
        private readonly AppContext _context;

        public CarStationContextWrapper(AppContext context)
        {
            _context = context;
            //if (!_CarStationsContext.CarStations.Any())
            //{
            //    _CarStationsContext.CarStations.Add(new CarStations_DAL(0, "Alex"));
            //    _CarStationsContext.CarStations.Add(new CarStations_DAL(1, "Dmytro"));
            //    _CarStationsContext.CarStations.Add(new CarStations_DAL(2, "Ihor"));
            //    _CarStationsContext.SaveChanges();
            //}
        }

        public async Task<ICarStation> Add(ICarStation item)
        {
            //int id = 0;
            //if (_context.Cars.Any())
            //{
            //    id = _context.Cars.Select(carStations => carStations.Id).Max() + 1;
            //}

            var newItem = new CarStation_DAL(item);
            await _context.CarStations.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }

        public async Task<IEnumerable<ICarStation>> Get()
        {
            return await Task.Run(() =>
            {
                return _context.CarStations;
            });
        }

        public async Task<ICarStation> GetById(int id)
        {
            return await Task.Run(() =>
            {
                return _context.CarStations.FirstOrDefault(carStations => carStations.Id == id);
            });
        }

        public async Task<bool> Put(int id, ICarStation item)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.CarStations.FirstOrDefault(carStations => carStations.Id == id);
                if (oldItem == null)
                {
                    return false;
                }

                _context.CarStations.Remove(oldItem);

                var newItem = new CarStation_DAL(item);
                await _context.CarStations.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return true;
            });
        }

        public async Task<ICarStation> Remove(int id)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.CarStations.FirstOrDefault(carStations => carStations.Id == id);
                if (oldItem == null)
                {
                    return null;
                }

                _context.CarStations.Remove(oldItem);
                await _context.SaveChangesAsync();
                return oldItem;
            });
        }

        //public ICarStationStation Add(ICarStationStation item)
        //{
        //    int id = 0;
        //    if (_context.CarStations.Any())
        //    {
        //        id = _context.CarStations.Select(CarStations => CarStations.Id).Max() + 1;
        //    }

        //    var newItem = new CarStation_DAL(id, item);
        //    _context.CarStations.Add(newItem);
        //    _context.SaveChanges();

        //    return newItem;
        //}

        //public IEnumerable<ICarStationStation> Get()
        //{
        //    return _context.CarStations;
        //}

        //public ICarStationStation GetById(int id)
        //{
        //    return _context.CarStations.FirstOrDefault(CarStations => CarStations.Id == id);
        //}

        //public bool Put(int id, ICarStationStation item)
        //{
        //    var oldItem = _context.CarStations.FirstOrDefault(CarStations => CarStations.Id == id);
        //    if (oldItem == null)
        //    {
        //        return false;
        //    }

        //    _context.CarStations.Remove(oldItem);

        //    var newItem = new CarStation_DAL(oldItem.Id, item);
        //    _context.CarStations.Add(newItem);
        //    _context.SaveChanges();
        //    return true;
        //}

        //public ICarStationStation Remove(int id)
        //{
        //    var oldItem = _context.CarStations.FirstOrDefault(CarStations => CarStations.Id == id);
        //    if (oldItem == null)
        //    {
        //        return null;
        //    }

        //    _context.CarStations.Remove(oldItem);

        //    return oldItem;
        //}
    }
}
