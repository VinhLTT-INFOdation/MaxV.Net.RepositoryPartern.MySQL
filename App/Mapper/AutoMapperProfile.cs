using App.Data.Entities;
using App.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRequest>();
            CreateMap<User, UserNonRequest>();
            CreateMap<Staff, StaffRequest>();
            CreateMap<Staff, StaffNonRequest>();
            CreateMap<Department, DepartmentRequest>();
            CreateMap<Department, DepartmentNonRequest>();
        }

    }
}
