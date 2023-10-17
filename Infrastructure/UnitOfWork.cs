using Application;
using Application.IRepositories.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SecondHandDbContext _db;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(SecondHandDbContext context, IServiceProvider serviceProvider) {
            _db= context;
            _serviceProvider= serviceProvider;
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them here
                throw;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
             return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
        }
    }
}
