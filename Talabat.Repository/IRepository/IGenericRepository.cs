using Talabat.Core.Entities;
using Talabat.Repository.Specifications;

namespace Talabat.Repository.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int? id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        Task<T?> GetWithSpecsAsync(ISpecifications<T> specs);

        Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specs);
        Task<int> CountAsync(ISpecifications<T> specs);
    }
}
