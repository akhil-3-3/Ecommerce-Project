using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoryRepository : ConnectionRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<CategoryResponseDto>(
                "sp_GetAllCategories",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<CategoryResponseDto>(
                "sp_GetCategoryById",
                new { CategoryId = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AddCategoryAsync(AddCategoryDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddCategory",
                new
                {
                    dto.CategoryName
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateCategory",
                new
                {
                    dto.CategoryId,
                    dto.CategoryName
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteCategory",
                new
                {
                    CategoryId = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}