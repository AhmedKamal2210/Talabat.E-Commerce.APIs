using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Sevices.Dtos.ProductDto
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

            return null;
        }
    }
}
