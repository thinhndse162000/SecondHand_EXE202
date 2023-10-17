using Application;
using Application.IRepositories;
using Application.IRepositories.Base;
using Application.IServices;
using Application.IServices.Base;
using Domain.JwtBearer;
using Domain.Models;
using Domain.Request.User;
using Infrastructure.Repositories;
using Infrastructure.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;


        public UserService( IUnitOfWork unitOfWork, IUserRepository userRepository) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            
        }
        public async Task<User?> GetByEmail(string Email)
        {
            return await _userRepository.GetByEmail(Email);
        }

        public async Task<JwtBearer> LoginAsync(User user)
        {
            return await _userRepository.ValidLogin(user);

        }
    }
}
