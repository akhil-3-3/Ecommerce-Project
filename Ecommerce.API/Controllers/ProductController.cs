using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var products = await _productRepository.GetAllProductsAsync();
                return Ok(products);
            }

            var result = await _productRepository.SearchProductsAsync(keyword);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDtos dto)
        {
            var id = await _productRepository.AddProductAsync(dto);

            return Ok(new
            {
                Message = "Product added successfully.",
                ProductId = id
            });
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDtos dto)
        {
            var rowsAffected = await _productRepository.UpdateProductAsync(dto);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Product updated successfully.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var rowsAffected = await _productRepository.DeleteProductAsync(id);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Product deleted successfully.");
        }
    }
}