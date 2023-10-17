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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SecondHandDbContext secondHandDbContext) : base(secondHandDbContext)
        {
        }
    }
}
