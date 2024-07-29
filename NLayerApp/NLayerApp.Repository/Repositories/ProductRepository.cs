using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Repository.Repositories
{

    //GenericRepository Sınıfını miras alarak, IGenericRepo'yu implemente etmeyerek kod fazlalığından kaçındım.
    //IProductRepository'i implemente ederek sadece GetProductsWithCategory metodunu implemente etmiş oldum.

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> GetProductsWithCategory()
        {

            //Eagar Loading.
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
