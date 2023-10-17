using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.Base
{
    public interface IGenericRepository<T> where T : class
    {
        // CRUD operations
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<T> Get(Guid id, string? include);
        Task<IEnumerable<T>> GetAll(string? include);
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate);
    }
}
