using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(int? id);

        Task<IReadOnlyList<Product>> GetAllProductsAsync();
    }
}