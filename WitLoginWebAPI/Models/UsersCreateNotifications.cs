using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WitLoginWebAPI.Models
{
    public class UsersCreateNotifications
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
}
