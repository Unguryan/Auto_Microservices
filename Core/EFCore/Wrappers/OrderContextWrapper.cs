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
    public class OrderContextWrapper : IBaseContextWrapper<IOrder>
    {
        private readonly AppContext _context;

        public OrderContextWrapper(AppContext context)
        {
            _context = context;
            //if (!_UserContext.Orders.Any())
            //{
            //    _UserContext.Orders.Add(new User_DAL(0, "Alex"));
            //    _UserContext.Orders.Add(new User_DAL(1, "Dmytro"));
            //    _UserContext.Orders.Add(new User_DAL(2, "Ihor"));
            //    _UserContext.SaveChanges();
            //}
        }

        public async Task<IOrder> Add(IOrder item)
        {
            //int id = 0;
            //if (_context.Cars.Any())
            //{
            //    id = _context.Orders.Select(order => order.Id).Max() + 1;
            //}

            var newItem = new Order_DAL(item);
            await _context.Orders.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }

        public async Task<IEnumerable<IOrder>> Get()
        {
            return await Task.Run(() =>
            {
                return _context.Orders;
            });
        }

        public async Task<IOrder> GetById(int id)
        {
            return await Task.Run(() =>
            {
                return _context.Orders.FirstOrDefault(order => order.Id == id);
            });
        }

        public async Task<bool> Put(int id, IOrder item)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.Orders.FirstOrDefault(order => order.Id == id);
                if (oldItem == null)
                {
                    return false;
                }

                _context.Orders.Remove(oldItem);

                var newItem = new Order_DAL(item);
                await _context.Orders.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return true;
            });
        }

        public async Task<IOrder> Remove(int id)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.Orders.FirstOrDefault(order => order.Id == id);
                if (oldItem == null)
                {
                    return null;
                }

                _context.Orders.Remove(oldItem);
                await _context.SaveChangesAsync();
                return oldItem;
            });
        }

        //public IOrder Add(IOrder item)
        //{
        //    int id = 0;
        //    if (_context.Orders.Any())
        //    {
        //        id = _context.Orders.Select(User => User.Id).Max() + 1;
        //    }

        //    var newItem = new Order_DAL(id, item);
        //    _context.Orders.Add(newItem);
        //    _context.SaveChanges();

        //    return newItem;
        //}

        //public IEnumerable<IOrder> Get()
        //{
        //    return _context.Orders;
        //}

        //public IOrder GetById(int id)
        //{
        //    return _context.Orders.FirstOrDefault(User => User.Id == id);
        //}

        //public bool Put(int id, IOrder item)
        //{
        //    var oldItem = _context.Orders.FirstOrDefault(User => User.Id == id);
        //    if (oldItem == null)
        //    {
        //        return false;
        //    }

        //    _context.Orders.Remove(oldItem);

        //    var newItem = new Order_DAL(oldItem.Id, item);
        //    _context.Orders.Add(newItem);
        //    _context.SaveChanges();
        //    return true;
        //}

        //public IOrder Remove(int id)
        //{
        //    var oldItem = _context.Orders.FirstOrDefault(User => User.Id == id);
        //    if (oldItem == null)
        //    {
        //        return null;
        //    }

        //    _context.Orders.Remove(oldItem);

        //    return oldItem;
        //}
    }
}
