using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItemResponseDto>> GetCartAsync(int userId);

        Task AddToCartAsync(AddCartItemDto dto, int userId);

        Task UpdateQuantityAsync(int cartItemId, int quantity);

        Task RemoveFromCartAsync(int cartItemId);

        Task ClearCartAsync(int userId);
    }
}