using Application.IServices;
using Application.IServices.Base;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService categoryService)
        {
            _service = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category entity)
        {
            entity.CategoryId = Guid.NewGuid();
            await _service.Create(entity);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, string? include)
        {
            var entity = await _service.Get(id,include);
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
        public virtual async Task<IActionResult> Update(Guid id, Category entity)
        {
            var idProperty = entity.GetType().GetProperty("CategoryId");

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
