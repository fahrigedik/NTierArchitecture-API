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
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = 5, categoryName = "Elektronik", CreatedDate = DateTime.Now });
            builder.HasData(new Category { Id = 6, categoryName = "Ev Eşyaları", CreatedDate = DateTime.Now });
        }
    }
}
