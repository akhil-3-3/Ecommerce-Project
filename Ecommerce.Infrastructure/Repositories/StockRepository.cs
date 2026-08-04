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

        public async Task<IEnumerable<StockResponseDto>> GetAllStockAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<StockResponseDto>(
                "sp_GetAllStock",
                commandType: CommandType.StoredProcedure);
        }

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

        public async Task<int> UpdateStockAsync(UpdateStockDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateStock",
                new
                {
                    dto.StockId,
                    dto.ProductId,
                    dto.Quantity
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteStockAsync(int stockId)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteStock",
                new
                {
                    StockId = stockId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}