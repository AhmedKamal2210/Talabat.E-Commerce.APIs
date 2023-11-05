using System.Collections;
using Talabat.Core.Entities;
using Talabat.Repository.IRepository;

namespace Talabat.Repository.Repsository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync(); 

        public void Dispose()
            => _dbContext.Dispose();


        // to Inject Irepositoryies inside IUnitOfWork But More Generic Using Hash Tabe (Key , Value).
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryIntance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)) , _dbContext);

                _repositories.Add(type, repositoryIntance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        } 

    }
}
