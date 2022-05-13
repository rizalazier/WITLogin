using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitLoginWebAPI.Models;

namespace WitLoginWebAPI.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(UsersLogin users);
    }
}