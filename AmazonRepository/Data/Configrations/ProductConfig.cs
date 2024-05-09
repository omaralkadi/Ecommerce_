using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonRepository.Data.Configrations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.PictureUrl).IsRequired();

            builder.HasOne(e => e.ProductBrand)
                .WithMany()
                .HasForeignKey(e => e.ProductBrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.ProductType)
                .WithMany()
                .HasForeignKey(e => e.ProductTypeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
