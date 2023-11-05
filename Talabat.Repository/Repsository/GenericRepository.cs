using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Entities.ProductEntities;
using Talabat.Repository.IRepository;
using Talabat.Repository.Specifications;

namespace Talabat.Repository.Repsository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            if (typeof(T) == typeof(Product))
                return await _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
            => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            =>  _dbContext.Set<T>().Remove(entity);

        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);



        #region With Specifications
        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).ToListAsync();

        public async Task<T?> GetWithSpecsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specs)
            => SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), specs);

        public async Task<int> CountAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).CountAsync();
        #endregion
    }
}
