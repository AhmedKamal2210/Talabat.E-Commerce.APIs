using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.ProductEntities;
using Talabat.Repository.IRepository;

namespace Talabat.Repository.Repsository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _dbContext;
        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
            => await _dbContext.Products.ToListAsync();

        public async Task<Product> GetProductAsync(int? id)
            => await _dbContext.Products.FindAsync(id);
    }
}
