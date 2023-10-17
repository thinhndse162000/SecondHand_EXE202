using Application;
using Application.IRepositories;
using Application.IServices;
using Domain.Models;
using Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository) : base(unitOfWork)
        {
            _repo = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllIncludeUser()
        {
            return await _repo.GetAllIncludeUser();
        }

        public async Task<Product> GetProductContainUser(Guid id)
        {
            return await _repo.GetProductContainUser(id);
        }
    }
}
