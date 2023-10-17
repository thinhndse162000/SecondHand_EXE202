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
    public class StorageRepository : GenericRepository<Storage>, IStorageRepository
    {
        private readonly SecondHandDbContext _db;

        public StorageRepository(SecondHandDbContext secondHandDbContext, SecondHandDbContext secondHandDbContext1) : base(secondHandDbContext)
        {
            _db = secondHandDbContext;
        }

        public async Task<Storage?> GetStorageByUserId(Guid id)
        {
            var storage = await _db.Storages.Where(p => p.UserId == id).FirstOrDefaultAsync();
            return storage;
        }
    }
}
