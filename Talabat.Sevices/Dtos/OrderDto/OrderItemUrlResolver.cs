using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Sevices.Dtos.OrderDto
{
    public class OrderItemUrlResolver : IValueResolver<OrderItems, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItems source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.ProductPictureUrl))
                return $"{_configuration["BaseUrl"]}{source.ItemOrdered.ProductPictureUrl}";

            return null;
        }
    }
}
