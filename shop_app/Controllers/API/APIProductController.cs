using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop_app.Models;
using shop_app.Services;

namespace shop_app.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class APIProductController : ControllerBase
    {
        private readonly IServiceProduct _serviceProduct;
        public APIProductController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _serviceProduct.ReadAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest("Product object is null");
            }
            _ = await _serviceProduct.CreateAsync(product);
            // return CreatedAtAction(nameof(CreateProduct), product);
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product object is null");
            }
            var updateProduct = await _serviceProduct.UpdateAsync(id, product);
            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteProduct = await _serviceProduct.DeleteAsync(id);
            if(!deleteProduct)
            {
                return NotFound();
            }
            return Ok(new { message = "Product is deleted"});
        }
    }
}
