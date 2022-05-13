using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WitLoginWebAPI.Models;

namespace WitLoginWebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsersRegister, Users>();
            CreateMap<UsersUpdate, Users>();
        }
    }
}
