using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Sevices.Dtos.ProductDto
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
                
        }
    }
}
