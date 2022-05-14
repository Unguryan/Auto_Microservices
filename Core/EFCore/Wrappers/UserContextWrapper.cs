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
    public class UserContextWrapper : IBaseContextWrapper<IUser>
    {
        private readonly AppContext _context;

        public UserContextWrapper(AppContext context)
        {
            _context = context;
            //if (!_UserContext.Users.Any())
            //{
            //    _UserContext.Users.Add(new User_DAL(0, "Alex"));
            //    _UserContext.Users.Add(new User_DAL(1, "Dmytro"));
            //    _UserContext.Users.Add(new User_DAL(2, "Ihor"));
            //    _UserContext.SaveChanges();
            //}
        }

        public async Task<IUser> Add(IUser item)
        {
            //int id = 0;
            //if (_context.Cars.Any())
            //{
            //    id = _context.Users.Select(user => user.Id).Max() + 1;
            //}

            var newItem = new User_DAL(item);
            await _context.Users.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }

        public async Task<IEnumerable<IUser>> Get()
        {
            return await Task.Run(() =>
            {
                return _context.Users;
            });
        }

        public async Task<IUser> GetById(int id)
        {
            return await Task.Run(() =>
            {
                return _context.Users.FirstOrDefault(user => user.Id == id);
            });
        }

        public async Task<bool> Put(int id, IUser item)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.Users.FirstOrDefault(user => user.Id == id);
                if (oldItem == null)
                {
                    return false;
                }

                _context.Users.Remove(oldItem);

                var newItem = new User_DAL(item);
                await _context.Users.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return true;
            });
        }

        public async Task<IUser> Remove(int id)
        {
            return await Task.Run(async() =>
            {
                var oldItem = _context.Users.FirstOrDefault(user => user.Id == id);
                if (oldItem == null)
                {
                    return null;
                }

                _context.Users.Remove(oldItem);
                await _context.SaveChangesAsync();
                return oldItem;
            });
        }

        //public IUser Add(IUser item)
        //{
        //    int id = 0;
        //    if (_context.Users.Any())
        //    {
        //        id = _context.Users.Select(User => User.Id).Max() + 1;
        //    }

        //    var newItem = new User_DAL(id, item);
        //    _context.Users.Add(newItem);
        //    _context.SaveChanges();

        //    return newItem;
        //}

        //public IEnumerable<IUser> Get()
        //{
        //    return _context.Users;
        //}

        //public IUser GetById(int id)
        //{
        //    return _context.Users.FirstOrDefault(User => User.Id == id);
        //}

        //public bool Put(int id, IUser item)
        //{
        //    var oldItem = _context.Users.FirstOrDefault(User => User.Id == id);
        //    if (oldItem == null)
        //    {
        //        return false;
        //    }

        //    _context.Users.Remove(oldItem);

        //    var newItem = new User_DAL(oldItem.Id, item);
        //    _context.Users.Add(newItem);
        //    _context.SaveChanges();
        //    return true;
        //}

        //public IUser Remove(int id)
        //{
        //    var oldItem = _context.Users.FirstOrDefault(User => User.Id == id);
        //    if (oldItem == null)
        //    {
        //        return null;
        //    }

        //    _context.Users.Remove(oldItem);

        //    return oldItem;
        //}
    }
}
