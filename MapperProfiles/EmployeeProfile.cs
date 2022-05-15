using AutoMapper;

using crud.DTOs;
using crud.Models;

namespace crud.MapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
        }
    }
}
