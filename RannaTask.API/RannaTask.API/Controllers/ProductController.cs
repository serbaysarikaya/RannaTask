using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RannaTask.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet("GetProducts")]
        [Produces("application/json")]
        public ActionResult<List<Product>> GetProducts()
        {
            return _productService.GetProducts();
        }

        [Authorize]
        [HttpGet("GetProduct/{id}")]
        [Produces("application/json")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [Authorize]
        [HttpPost("AddProduct")]
        [Produces("application/json")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Invalid product data.");

            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [Authorize]
        [HttpPut("UpdateProduct/{id}")]
        [Produces("application/json")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {

            if (id != product.Id)
                return BadRequest();

            _productService.UpdateProduct(product);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
