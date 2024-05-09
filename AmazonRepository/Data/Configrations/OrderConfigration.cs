using AmazonCore.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonRepository.Data.Configrations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");
            builder.OwnsOne(e => e.ShippingAddress, o => o.WithOwner());

            builder.Property(e => e.Status).HasConversion(Ostatus => Ostatus.ToString(),
                                              Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));

            builder.HasOne(e=>e.deliveryMethod)
                .WithMany().OnDelete(DeleteBehavior.NoAction);


        }
    }
}
