using Application.IRepositories;
using Domain.JwtBearer;
using Domain.Models;
using Domain.Request.User;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SecondHandDbContext _db;
        private readonly IConfiguration config;
        public UserRepository(SecondHandDbContext secondHandDbContext, IConfiguration configuration) : base(secondHandDbContext)
        {
            _db= secondHandDbContext;
            config = configuration;
        }

        public async Task<User?> GetByEmail(string Email)
        {
            var user = await _db.Users.Where(p => p.Email == Email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<JwtBearer> ValidLogin(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtBearer:Key"]));
                var token = new JwtSecurityToken
                        (
                            issuer: config["JwtBearer: Issuer"],
                            audience: config["JwtBearer: Audience"],
                            expires: DateTime.Now.AddMinutes(20),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                        );
            JwtBearer jwtBearer = new JwtBearer();
            jwtBearer.token = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtBearer;
        }
    }
}
