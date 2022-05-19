using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WitLoginWebAPI.Entities;

namespace WitLoginWebAPI.Helpers
{
    public class SQLDataContext : DbContext
    {
        protected readonly IConfiguration iconfiguration;

        public SQLDataContext(IConfiguration configuration)
        {
            iconfiguration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(iconfiguration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<DbUsersCreateNotification> UsersCreateNotifications { get; set; }
        public DbSet<DbUsers> Users { get; set; }
    }
}
