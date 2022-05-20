using AutoMapper;

using crud.DTOs;
using crud.Models;

namespace crud.MapperProfiles
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<Pagination<EmployeeDto>, Pagination<Employee>>();
            CreateMap<Pagination<EmployeeDto>, Pagination<Employee>>().ReverseMap();
        }
    }
}
