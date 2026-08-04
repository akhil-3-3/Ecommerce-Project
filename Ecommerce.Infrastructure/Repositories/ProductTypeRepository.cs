using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductTypeRepository : ConnectionRepository, IProductTypeRepository
    {
        public ProductTypeRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<ProductTypeResponseDto>> GetAllProductTypesAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<ProductTypeResponseDto>(
                "sp_GetAllProductTypes",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<ProductTypeResponseDto?> GetProductTypeByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<ProductTypeResponseDto>(
                "sp_GetProductTypeById",
                new { ProductTypeId = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AddProductTypeAsync(AddProductTypeDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddProductType",
                new { dto.ProductTypeName },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateProductTypeAsync(UpdateProductTypeDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateProductType",
                new
                {
                    dto.ProductTypeId,
                    dto.ProductTypeName
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteProductTypeAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteproductType",
                new { ProductTypeId = id},
                commandType: CommandType.StoredProcedure);  
        }


    }
}
