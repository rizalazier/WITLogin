using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WitLoginWebAPI.Entities
{
    public class DbUsersCreateNotification
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
    }
}
