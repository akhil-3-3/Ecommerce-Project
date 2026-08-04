using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDto dto)
        {
            var id = await _reviewService.AddReviewAsync(dto);

            return Ok(new
            {
                Message = "Review added successfully.",
                ReviewId = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto dto)
        {
            var rows = await _reviewService.UpdateReviewAsync(dto);

            if (rows == 0)
                return NotFound();

            return Ok("Review updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var rows = await _reviewService.DeleteReviewAsync(id);

            if (rows == 0)
                return NotFound();

            return Ok("Review deleted successfully.");
        }
    }
}