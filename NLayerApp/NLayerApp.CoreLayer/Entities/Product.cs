using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities
{
    public class Product : BaseEntity
    {
        public string? productName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category? Category { get; set; }
        public ProductFeature? productFeature { get; set; }
    }
}
