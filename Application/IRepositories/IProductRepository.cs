using Application.IRepositories.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductContainUser(Guid id);
        Task<IEnumerable<Product>> GetAllIncludeUser();
    }
}
