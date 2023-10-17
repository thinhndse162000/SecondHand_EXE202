using Application.IServices.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IProductService: IGenericService<Product>
    {
        Task<Product> GetProductContainUser(Guid id);
        Task<IEnumerable<Product>> GetAllIncludeUser();
    }
}
