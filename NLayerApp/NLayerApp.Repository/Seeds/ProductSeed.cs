using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                productName = "İphone 13",
                Price = 40000,
                Stock = 50,
                CreatedDate = DateTime.Now,
                categoryId = 5
            });

            builder.HasData(new Product
            {
                Id = 2,
                productName = "İphone 14",
                Price = 50000,
                Stock = 50,
                CreatedDate = DateTime.Now,
                categoryId = 5
            });

            builder.HasData(new Product
            {
                Id = 3,
                productName = "Dyson Elektrikli Süpürge",
                Price = 50000,
                Stock = 50,
                CreatedDate = DateTime.Now,
                categoryId = 6
            });
        }
    }
}
