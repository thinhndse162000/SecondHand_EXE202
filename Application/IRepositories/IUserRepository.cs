using Application.IRepositories.Base;
using Domain.JwtBearer;
using Domain.Models;
using Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string Email);
        Task<JwtBearer> ValidLogin(User user);
    }
}
