﻿using NLayerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //IGenericRepository'dekiler Miras Alındı.

        Task<List<Product>> GetProductsWithCategory();


    }
}
