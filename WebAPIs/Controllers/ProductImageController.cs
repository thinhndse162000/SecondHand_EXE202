using Application.IServices;
using Application.IServices.Base;
using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/image")]
    public class ProductImageController :   Controller
    {
        private readonly IProductImageService _service;

        public ProductImageController(IProductImageService productImageService) {
            _service = productImageService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductImage entity)
        {
            entity.ImageId = Guid.NewGuid();
            await _service.Create(entity);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, string? include)
        {
            var entity = await _service.Get(id, include);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? include)
        {
            var entities = await _service.GetAll(include);
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, ProductImage entity)
        {
            var idProperty = entity.GetType().GetProperty("ImageId");

            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                // Get the value of the 'Id' property
                var idValue = (Guid)idProperty.GetValue(entity);

                if (id.Equals(idValue))
                {
                    await _service.Update(entity);
                    return Ok();
                }
            }
            return BadRequest("Wrong id");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
