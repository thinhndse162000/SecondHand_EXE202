using Application.IRepositories.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly SecondHandDbContext _db;
        public GenericRepository(SecondHandDbContext secondHandDbContext){ 
            _db = secondHandDbContext;
        }
        public async Task Create(T entity)
        {

            await _db.Set<T>().AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            _db.Set<T>().Remove((await _db.Set<T>().FindAsync(id))!);
        }

        public async Task<T> Get(Guid id, string? include = null)
        {
            var query = _db.Set<T>().AsQueryable();
            if (!string.IsNullOrEmpty(include))
            {
                // Split the include parameter value into an array of related entity names
                var relatedEntities = include.Split(',');

                // Include the related entities in the query
                foreach (var entityName in relatedEntities)
                {
                    query = query.Include(entityName);
                }
                
            }
            return query.AsEnumerable().FirstOrDefault(entity => entity.GetType().GetProperty(typeof(T).Name + "Id").GetValue(entity).ToString().Equals(id.ToString()))!;
        }

        public async Task<IEnumerable<T>> GetAll(string? include = null)
        {
            // Create a queryable instance for the entity of type T
            var query = _db.Set<T>().AsQueryable();

            if (!string.IsNullOrEmpty(include))
            {
                // Split the include parameter value into an array of related entity names
                var relatedEntities = include.Split(',');

                // Include the related entities in the query
                foreach (var entityName in relatedEntities)
                {
                    query = query.Include(entityName);
                }
            }

            // Execute the query and return the results as a list
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().Where(predicate).Select(u => u).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
    }
}
