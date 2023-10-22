using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly SecondHandDbContext _db;

        public ProductRepository(SecondHandDbContext secondHandDbContext, SecondHandDbContext db) : base(secondHandDbContext)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllIncludeUser()
        {
            return await _db.Products.Include(storage => storage.Storage)
                                    .ThenInclude(user => user.User).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProdctByCategory(Guid id)
        {
            var product = await _db.Products.Where(p=> p.CategoryId == id).ToListAsync();
            return product;
        }

        public async Task<Product> GetProductContainUser(Guid id)
        {
            var product = await _db.Products.Where(p=>p.ProductId == id).Include(storage => storage.Storage)
                                            .ThenInclude(user => user.User).Where(p => p.ProductId == id).FirstOrDefaultAsync();
            return product;
        }
    }
}
