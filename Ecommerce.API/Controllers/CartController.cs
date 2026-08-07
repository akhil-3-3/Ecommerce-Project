using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private int UserId =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync(UserId);

            return Ok(cart);
        }
        [EnableRateLimiting("fixed")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddCartItemDto dto)
        {
            await _cartService.AddToCartAsync(dto, UserId);

            return Ok(new
            {
                message = "Product added to cart."
            });
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [FromBody] int quantity)
        {
            await _cartService.UpdateQuantityAsync(cartItemId, quantity);

            return Ok(new
            {
                message = "Quantity updated."
            });
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);

            return Ok(new
            {
                message = "Item removed."
            });
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync(UserId);

            return Ok(new
            {
                message = "Cart cleared."
            });
        }
    }
}