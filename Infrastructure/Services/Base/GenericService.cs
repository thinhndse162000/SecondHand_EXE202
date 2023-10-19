using Application;
using Application.IRepositories.Base;
using Application.IServices.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Base
{
    public class GenericService<T> : IGenericService<T> where T : class
    {

        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IUnitOfWork unitOfWork) { 

            _unitOfWork = unitOfWork;
        }
        public async Task Create(T entity)
        {
            await _unitOfWork.GetRepository<T>().Create(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.GetRepository<T>().Delete(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<T> Get(Guid id, string? include = null)
        {
           return await _unitOfWork.GetRepository<T>().Get(id, include);
        }

        public async Task<IEnumerable<T>> GetAll(string? include)
        {
            return await _unitOfWork.GetRepository<T>().GetAll(include);
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<T>().Filter(predicate);
        }

        public async Task Update(T entity)
        {
            await _unitOfWork.GetRepository<T>().Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> Count()
        {
            return await _unitOfWork.GetRepository<T>().Count();
        }
    }
}
