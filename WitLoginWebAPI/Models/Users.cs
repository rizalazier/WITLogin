using System;

namespace WitLoginWebAPI.Models
{
    public class Users
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
