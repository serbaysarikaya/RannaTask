using AutoMapper;
using RannaTask.Entities.Concrete;
using RannaTask.Entities.Dtos;
using RannaTask.Shared.Utilities.Results.Concrete;

namespace RannaTask.Services.AutoMapper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddDto, Product>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<ProductUpdateDto, Product>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductDto, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, ProductDto>();
            CreateMap<DataResult<ProductDto>, Product>();

        }
    }
}
