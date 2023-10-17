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
    public class StorageService : GenericService<Storage>, IStorageService
    {
        private readonly IStorageRepository _repo;

        public StorageService(IUnitOfWork unitOfWork, IStorageRepository storageRepository) : base(unitOfWork)
        {
            _repo = storageRepository;
        }

        public async Task<Storage> GetStorageByUserId(Guid id)
        {
            return await _repo.GetStorageByUserId(id);
        }
    }
}
