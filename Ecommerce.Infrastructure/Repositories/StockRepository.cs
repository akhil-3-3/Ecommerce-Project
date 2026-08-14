using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class StockRepository : ConnectionRepository, IStockRepository
    {
        public StockRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        // ==========================
        // GET ALL STOCK
        // ==========================

        public async Task<IEnumerable<StockResponseDto>> GetAllStockAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<StockResponseDto>(
                "sp_GetAllStock",
                commandType: CommandType.StoredProcedure);
        }

        // ==========================
        // GET STOCK BY PRODUCT
        // ==========================

        public async Task<StockResponseDto?> GetStockByProductIdAsync(int productId)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<StockResponseDto>(
                "sp_GetStockByProductId",
                new
                {
                    ProductId = productId
                },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================
        // ADD STOCK
        // ==========================

        public async Task<int> AddStockAsync(AddStockDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddStock",
                new
                {
                    dto.ProductId,
                    dto.Quantity
                },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================
        // UPDATE STOCK
        // ==========================

        public async Task<int> UpdateStockAsync(UpdateStockDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateStock",
                new
                {
                    dto.ProductId,
                    dto.Quantity
                },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================
        // DELETE STOCK
        // ==========================

        public async Task<int> DeleteStockAsync(int productId)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteStock",
                new
                {
                    ProductId = productId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}