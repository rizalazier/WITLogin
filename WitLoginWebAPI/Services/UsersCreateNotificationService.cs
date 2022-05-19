using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitLoginWebAPI.Entities;
using WitLoginWebAPI.Helpers;

namespace WitLoginWebAPI.Services
{
    public interface IUsersCreateNotificationService
    {
        IEnumerable<DbUsersCreateNotification> GetAll();
        DbUsersCreateNotification GetByUsername(string username);
        DbUsersCreateNotification Create(DbUsersCreateNotification userNotification);        
    }

    public class UsersCreateNotificationService : IUsersCreateNotificationService
    {
        private SQLDataContext _context;

        public UsersCreateNotificationService(SQLDataContext context)
        {
            _context = context;
        }

        public IEnumerable<DbUsersCreateNotification> GetAll()
        {
            return _context.UsersCreateNotifications;
        }

        public DbUsersCreateNotification GetByUsername(string username)
        {
            return _context.UsersCreateNotifications.Where(x => x.Username == username).FirstOrDefault();
        }

        public DbUsersCreateNotification Create(DbUsersCreateNotification userNotification)
        {

            var user = _context.Users.Where(x => x.Username == userNotification.Username).FirstOrDefault();

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (string.IsNullOrWhiteSpace(userNotification.Message))
            {
                throw new AppException("Message is required");
            }


            userNotification.Id = Guid.NewGuid();

            _context.UsersCreateNotifications.Add(userNotification);
            _context.SaveChanges();

            return userNotification;
        }
    }
}
