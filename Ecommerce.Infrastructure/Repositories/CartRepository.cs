using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CartRepository : ConnectionRepository, ICartRepository
    {
        public CartRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetCartAsync(int userId)
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<CartItemResponseDto>(
                "sp_GetCart",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task AddToCartAsync(AddCartItemDto dto, int userId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_AddToCart",
                new
                {
                    UserId = userId,
                    dto.ProductId,
                    dto.Quantity
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateQuantityAsync(int cartItemId, int quantity)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_UpdateCartQuantity",
                new
                {
                    CartItemId = cartItemId,
                    Quantity = quantity
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_RemoveCartItem",
                new
                {
                    CartItemId = cartItemId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ClearCartAsync(int userId)
        {
            using var connection = GetConnection();

            await connection.ExecuteAsync(
                "sp_ClearCart",
                new
                {
                    UserId = userId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}