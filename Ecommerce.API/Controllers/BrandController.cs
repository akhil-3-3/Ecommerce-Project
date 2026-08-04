using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(AddBrandDto dto)
        {
            var id = await _brandService.AddBrandAsync(dto);

            return Ok(new
            {
                Message = "Brand added successfully.",
                BrandId = id

            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto dto)
        {
            var rowsAffected = await _brandService.UpdateBrandAsync(dto);

            if (rowsAffected == 0)
                return NotFound();
            return Ok("Brand updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var rowsAffected = await _brandService.DeleteBrandAsync(id);

            if (rowsAffected == 0)
                return NotFound();
            return Ok("Brand deleted successfully.");
        }

    }
}
