using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _imageService.GetAllImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);

            if (image == null)
                return NotFound();

            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(AddImageDto dto)
        {
            var id = await _imageService.AddImageAsync(dto);

            return Ok(new
            {
                Message = "Image added successfully.",
                ImageId = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage(UpdateImageDto dto)
        {
            var rowsAffected = await _imageService.UpdateImageAsync(dto);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Image updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var rowsAffected = await _imageService.DeleteImageAsync(id);

            if (rowsAffected == 0)
                return NotFound();

            return Ok("Image deleted successfully.");
        }
    }
}