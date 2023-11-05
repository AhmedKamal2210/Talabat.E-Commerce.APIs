using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Repository
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfigurations());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfigurations());
            //modelBuilder.ApplyConfiguration(new ProductCategoryConfigurations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProducatCategories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<DeliveryMethods> DeliveryMethods { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
