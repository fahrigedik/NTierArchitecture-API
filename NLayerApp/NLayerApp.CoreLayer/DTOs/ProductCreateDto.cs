﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.DTOs
{
    public class ProductCreateDto
    {
        public string? productName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int categoryId { get; set; }
    }
}
