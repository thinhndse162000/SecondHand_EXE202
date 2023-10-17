using Application.IRepositories;
using Domain.Models;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(SecondHandDbContext secondHandDbContext) : base(secondHandDbContext)
        {
        }
    }
}
