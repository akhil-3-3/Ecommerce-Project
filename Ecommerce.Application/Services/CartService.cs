using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetCartAsync(int userId)
        {
            return await _cartRepository.GetCartAsync(userId);
        }

        public async Task AddToCartAsync(AddCartItemDto dto, int userId)
        {
            await _cartRepository.AddToCartAsync(dto, userId);
        }

        public async Task UpdateQuantityAsync(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateQuantityAsync(cartItemId, quantity);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartRepository.RemoveFromCartAsync(cartItemId);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
    }
}