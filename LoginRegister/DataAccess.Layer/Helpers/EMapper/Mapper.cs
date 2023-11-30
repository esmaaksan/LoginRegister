using AutoMapper;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Helpers.EMapper
{
    public class Mapper:Profile
    {
        public Mapper() 
        {
            CreateMap<User, UsersDTO>().ReverseMap();
            CreateMap<User, loginDTO>().ReverseMap();
            CreateMap<User, PasswordDTO>().ReverseMap();
            CreateMap<User, ForgotPasswordDTO>().ReverseMap();
            CreateMap<Adresses, AddresesDTO>().ReverseMap();
        }
    }
}
