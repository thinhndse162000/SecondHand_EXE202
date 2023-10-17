﻿using Application.IServices;
using Application.IServices.Base;
using Domain.Models;
using Domain.Request.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        public UserController(IUserService userService, IStorageService storageService)
        {
            _userService = userService;
            _storageService = storageService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(User entity)
        {
            entity.UserId = Guid.NewGuid();
            entity.Role = "User";
            await _userService.Create(entity);
            Storage storage = new Storage();
            storage.StorageId = Guid.NewGuid();
            storage.UserId = entity.UserId;
            await _storageService.Create(storage);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, string? include)
        {
            var entity = await _userService.Get(id, include);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? include)
        {
            var entities = await _userService.GetAll(include);
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, User entity)
        {
            var idProperty = entity.GetType().GetProperty("UserId");

            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                // Get the value of the 'Id' property
                var idValue = (Guid)idProperty.GetValue(entity);

                if (id.Equals(idValue))
                {
                    await _userService.Update(entity);
                    return Ok();
                }
            }
            return BadRequest("Wrong id");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidLogin(LoginRequest loginRequest) {
            var user = await _userService.GetByEmail(loginRequest.Email);
            if(user != null && user.Password == loginRequest.Password) {
                return Ok(await _userService.LoginAsync(user));
            }
            return Unauthorized();
        }
    }
}
