using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WitLoginWebAPI.Entities;
using WitLoginWebAPI.Models;

namespace WitLoginWebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<UsersRegister, Users>();
            //CreateMap<UsersUpdate, Users>();
            CreateMap<UsersRegister, DbUsers>();
            CreateMap<UsersUpdate, DbUsers>();
            CreateMap<Users, DbUsers>();
            CreateMap<DbUsers, Users>();
            CreateMap<UsersCreateNotifications, DbUsersCreateNotification>();
            CreateMap<DbUsersCreateNotification, UsersCreateNotifications>();
        }
    }
}
