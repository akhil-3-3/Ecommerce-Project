using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.DTOs;

namespace Ecommerce.Infrastructure.Repositories
{
    public class BrandRepository : ConnectionRepository, IBrandRepository
    {
        public BrandRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<BrandResponseDto>(
                "sp_GetAllBrands",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<BrandResponseDto?> GetBrandByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<BrandResponseDto>(
                "sp_GetBrandById",
                new { BrandId = id },
                commandType: CommandType.StoredProcedure);
        }


        public async Task<int> AddBrandAsync(AddBrandDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddBrand",
                new
                {
                    dto.BrandName
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateBrandAsync(UpdateBrandDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateBrand",
                new
                {
                    dto.BrandId,
                    dto.BrandName
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteBrandAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteBrand",
                new
                {
                    BrandId = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
