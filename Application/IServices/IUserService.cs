using Application.IServices.Base;
using Domain.JwtBearer;
using Domain.Models;
using Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService : IGenericService<User>
    {

        Task<JwtBearer> LoginAsync(User user);
        Task<User> GetByEmail(string Email);
    }
}
