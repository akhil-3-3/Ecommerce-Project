using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productTypeService.GetAllProductTypesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _productTypeService.GetProductTypeByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductTypeDto dto)
        {
            var id = await _productTypeService.AddProductTypeAsync(dto);

            return Ok(new
            {
                Message = "Product Type Added Successfully",
                ProductTypeId = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductTypeDto dto)
        {
            var rows = await _productTypeService.UpdateProductTypeAsync(dto);

            if (rows == 0)
                return NotFound();

            return Ok("Product Type Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rows = await _productTypeService.DeleteProductTypeAsync(id);

            if (rows == 0)
                return NotFound();

            return Ok("Product Type Deleted Successfully");
        }
    }
}