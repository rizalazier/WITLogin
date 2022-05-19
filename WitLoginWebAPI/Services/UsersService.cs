using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitLoginWebAPI.Entities;
using WitLoginWebAPI.Helpers;

namespace WitLoginWebAPI.Services
{
    public interface IUsersService
    {
        DbUsers Authenticate(string username, string password);
        IEnumerable<DbUsers> GetAll();
        DbUsers GetById(string username);
        DbUsers Create(DbUsers user, string password);
        void Update(DbUsers user, string password = null);
        void Delete(Guid id);
    }

    public class UsersService : IUsersService
    {
        private SQLDataContext _context;

        public UsersService(SQLDataContext context)
        {
            _context = context;
        }

        public DbUsers Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null) 
            {
                return null;
            }

            if (user.Password.ToString() != password) 
            {
                return null;
            }

            return user;
        }

        public IEnumerable<DbUsers> GetAll()
        {
            return _context.Users;
        }

        public DbUsers GetById(string username)
        {
            return _context.Users.Where(x => x.Username == username).FirstOrDefault();
        }

        public DbUsers Create(DbUsers user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (_context.Users.Any(x => x.Username == user.Username))
            {
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            }


            user.id = Guid.NewGuid();

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(DbUsers userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                if (_context.Users.Any(x => x.Username == userParam.Username))
                {
                    throw new AppException("Username " + userParam.Username + " is already taken");
                }

                user.Username = userParam.Username;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Name))
            {
                user.Name = userParam.Name;
            }


            if (!string.IsNullOrWhiteSpace(password))
            {
                user.Password = password;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
