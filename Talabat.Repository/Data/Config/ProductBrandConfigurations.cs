using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.ProductEntities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(b => b.Name).IsRequired();
        }
    }
}
