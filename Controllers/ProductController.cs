using LaundryApi.Interfaces;
using LaundryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll() =>
            Ok(await _productService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(long id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var newProduct = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Product product)
        {
            var success = await _productService.UpdateAsync(id, product);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var success = await _productService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
