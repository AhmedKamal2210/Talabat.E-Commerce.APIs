using AutoMapper;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Sevices.Dtos.OrderDto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address , AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();
            CreateMap<Orders, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, option => option.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, option => option.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.ProductPictureUrl, option => option.MapFrom(src => src.ItemOrdered.ProductPictureUrl))
                .ForMember(dest => dest.ProductPictureUrl, option => option.MapFrom<OrderItemUrlResolver>()).ReverseMap();

        }
    }
}
