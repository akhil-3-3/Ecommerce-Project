using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task AddToWishlistAsync(int userId, int productId)
        {
            await _wishlistRepository.AddToWishlistAsync(userId, productId);
        }

        public async Task<IEnumerable<WishlistResponseDto>> GetWishlistAsync(int userId)
        {
            return await _wishlistRepository.GetWishlistAsync(userId);
        }

        public async Task RemoveWishlistItemAsync(int wishlistItemId)
        {
            await _wishlistRepository.RemoveWishlistItemAsync(wishlistItemId);
        }

        public async Task ClearWishlistAsync(int userId)
        {
            await _wishlistRepository.ClearWishlistAsync(userId);
        }
    }
}