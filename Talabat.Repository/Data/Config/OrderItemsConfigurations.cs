using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Data.Config
{
    public class OrderItemsConfigurations : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(orderItem => orderItem.ItemOrdered, x =>
            {
                x.WithOwner();
            });
        }
    }
}
