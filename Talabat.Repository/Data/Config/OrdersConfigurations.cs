using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderEntities;

namespace Talabat.Repository.Data.Config
{
    public class OrdersConfigurations : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, x =>
            {
                x.WithOwner(); // no table for shippingaddress in database
            });

            builder.HasMany(order => order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade); //when remove order , orderitems will be remove with it.



            
        }
    }
}
