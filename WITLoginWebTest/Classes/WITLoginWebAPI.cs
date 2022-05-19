using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitLoginWebAPI.Entities;
using WitLoginWebAPI.Services;

namespace WITLoginWebTest.Classes
{
    public class WITLoginWebAPIUsers : IUsersService
    {
        private IUsersService _usersService;
        public WITLoginWebAPIUsers(IUsersService usersService) 
        {
            _usersService = usersService;
        }


        public DbUsers Authenticate(string username, string password)
        {
            var userTest = _usersService.Authenticate(username, password);
            return userTest;
        }

        public DbUsers Create(DbUsers user, string password)
        {
            var userTest = _usersService.Create(user, password);
            return userTest;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbUsers> GetAll()
        {
            throw new NotImplementedException();
        }

        public DbUsers GetById(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(DbUsers user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
