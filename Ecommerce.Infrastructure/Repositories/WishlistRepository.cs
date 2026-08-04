using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class WishlistRepository : ConnectionRepository, IWishlistRepository
    {
        public WishlistRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task AddToWishlistAsync(int userId, int productId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_AddToWishlist",
                new
                {
                    UserId = userId,
                    ProductId = productId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<WishlistResponseDto>> GetWishlistAsync(int userId)
        {
            using var connection = GetConnection();

            var wishlist = new Dictionary<int, WishlistResponseDto>();

            await connection.QueryAsync<WishlistResponseDto, ImageResponseDto, WishlistResponseDto>(
                "sp_GetWishlist",
                (item, image) =>
                {
                    if (!wishlist.TryGetValue(item.WishlistItemId, out var existing))
                    {
                        existing = item;
                        existing.Images = new List<ImageResponseDto>();

                        wishlist.Add(existing.WishlistItemId, existing);
                    }

                    if (image != null && image.ImageId != 0)
                    {
                        existing.Images.Add(image);
                    }

                    return existing;
                },
                new
                {
                    UserId = userId
                },
                splitOn: "ImageId",
                commandType: CommandType.StoredProcedure);

            return wishlist.Values;
        }

        public async Task RemoveWishlistItemAsync(int wishlistItemId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_RemoveWishlistItem",
                new
                {
                    WishlistItemId = wishlistItemId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ClearWishlistAsync(int userId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_ClearWishlist",
                new
                {
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}