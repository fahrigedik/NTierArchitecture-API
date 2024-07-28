using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Entities
{
    public class Category  : BaseEntity
    {

        public string categoryName {  get; set; }

        public ICollection<Product> Products { get; set; } //navigation property


    }
}
