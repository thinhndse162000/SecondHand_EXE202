using Application.IServices;
using Application.IServices.Base;
using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/storage")]
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;
        public StorageController(IStorageService storageService) {
            _storageService = storageService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Storage entity)
        {
            entity.StorageId = Guid.NewGuid();
            await _storageService.Create(entity);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, string? include)
        {
            var entity = await _storageService.Get(id, include);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? include)
        {
            var entities = await _storageService.GetAll(include);
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, Storage entity)
        {
            var idProperty = entity.GetType().GetProperty("StorageId");

            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                // Get the value of the 'Id' property
                var idValue = (Guid)idProperty.GetValue(entity);

                if (id.Equals(idValue))
                {
                    await _storageService.Update(entity);
                    return Ok();
                }
            }
            return BadRequest("Wrong id");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _storageService.Delete(id);
            return NoContent();
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetStorageByUserId(Guid id) 
        {
            var entity = await _storageService.GetStorageByUserId(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }
    }
}
