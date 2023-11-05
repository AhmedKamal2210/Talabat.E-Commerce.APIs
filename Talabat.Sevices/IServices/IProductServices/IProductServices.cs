using Talabat.Repository.Specifications;
using Talabat.Sevices.Dtos.ProductDto;
using Talabat.Sevices.Helpers;

namespace Talabat.Sevices.IServices.IProductServices
{
    public interface IProductServices
    {
        Task<ProductDto> GetProductAsync(int? id);

        Task<Pagination<ProductDto>> GetAllProductsAsync(ProductSpecifications productSpecs);

    }
}
