using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IWishlistRepository
    {
        Task AddToWishlistAsync(int userId, int productId);

        Task<IEnumerable<WishlistResponseDto>> GetWishlistAsync(int userId);

        Task RemoveWishlistItemAsync(int wishlistItemId);

        Task ClearWishlistAsync(int userId);
    }
}