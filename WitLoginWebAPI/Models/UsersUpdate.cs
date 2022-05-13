using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WitLoginWebAPI.Models
{
    public class UsersUpdate
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
