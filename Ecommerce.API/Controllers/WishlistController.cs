using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        private int UserId =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var wishlist = await _wishlistService.GetWishlistAsync(UserId);
            return Ok(wishlist);
        }
        [EnableRateLimiting("fixed")]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AddWishlistDto dto)
        {
            await _wishlistService.AddToWishlistAsync(UserId, dto.ProductId);
            return Ok(new { message = "Added to wishlist" });
        }

        [HttpDelete("{wishlistItemId}")]
        public async Task<IActionResult> Remove(int wishlistItemId)
        {
            await _wishlistService.RemoveWishlistItemAsync(wishlistItemId);
            return Ok(new { message = "Removed from wishlist" });
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            await _wishlistService.ClearWishlistAsync(UserId);
            return Ok(new { message = "Wishlist cleared" });
        }
    }
}