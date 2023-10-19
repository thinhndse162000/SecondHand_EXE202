using Application.IServices;
using Application.IServices.Base;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService productService) {
            _service = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product entity)
        {
            entity.ProductId = Guid.NewGuid();
            entity.Category = null;
            entity.Storage = null;
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
        public virtual async Task<IActionResult> Update(Guid id, Product entity)
        {
            var idProperty = entity.GetType().GetProperty("ProductId");

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

        [HttpGet("toUser/{id}")]
        public async Task<IActionResult> GetProductToUser(Guid id)
        {
            var entity = await _service.GetProductContainUser(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet("include")]
        public async Task<IActionResult> GetProduct()
        {
            var entities = await _service.GetAllIncludeUser();
            return Ok(entities);
        }
        [HttpGet("filter")]

        public async Task<IActionResult> FilterProduct(string? ProductName, string? Size) {

            Expression<Func<Product, bool>> predicate = null;
            if (ProductName != null)
            {
                predicate = u => u.ProductName.Equals(ProductName);
            }
            else if (Size != null) {
                predicate = u => u.Size == Size;
            }
            var entity = await _service.Filter(predicate);
            return Ok(entity);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProductByCategoryId(Guid id) {
            var entity = await _service.GetProdctByCategory(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }
        [HttpGet("count")]
        public async Task<IActionResult> CountProduct()
        {
             
            return Ok(await _service.Count());
        }
    }
}
