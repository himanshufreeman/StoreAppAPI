
using AutoMapper;
using StoreAppAPI.DTOs;
using StoreAppAPI.Model;

namespace StoreAppAPI.MappingClasses
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductdetailsDTO,ProductModel>().ReverseMap();
        }
    }
}
