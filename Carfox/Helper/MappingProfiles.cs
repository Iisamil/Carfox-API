using AutoMapper;
using Carfox.Api.Dtos;
using Carfox.Core.Entities;
using Carfox.Dtos;

namespace Carfox.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<GetProductDto, Product>().ReverseMap()
            .ForMember(d => d.BrandName, O => O.MapFrom(s => s.ProductBrand.name))
            .ForMember(d => d.Logo, O => O.MapFrom(s => s.ProductBrand.logo));



            CreateMap<ProductBrand, BrandsDto>();
        }
    }
}
