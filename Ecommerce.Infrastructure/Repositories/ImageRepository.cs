using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ImageRepository : ConnectionRepository, IImageRepository
    {
        public ImageRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<ImageResponseDto>(
                "sp_GetAllImages",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<ImageResponseDto?> GetImageByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<ImageResponseDto>(
                "sp_GetImageById",
                new
                {
                    ImageId = id
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<ImageResponseDto?> GetImageByProductIdAsync(int productId)
{
    using var connection = GetConnection();

    return await connection.QueryFirstOrDefaultAsync<ImageResponseDto>(
        "sp_GetImageByProductId",
        new
        {
            ProductId = productId
        },
        commandType: CommandType.StoredProcedure);
}

        public async Task<int> AddImageAsync(ImageDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddImage",
                new
                {
                    dto.ProductId,
                    dto.ImageUrl,
                    dto.ImagePublicId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateImageAsync(UpdateImageDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateImage",
                new
                {
                    dto.ImageId,
                    dto.ProductId,
                    dto.ImageUrl,
                    dto.ImagePublicId

                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteImageAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteImage",
                new
                {
                    ImageId = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}