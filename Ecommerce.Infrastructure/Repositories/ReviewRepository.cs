using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ReviewRepository : ConnectionRepository, IReviewRepository
    {
        public ReviewRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<ReviewResponseDto>(
                "sp_GetAllReviews",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<ReviewResponseDto?> GetReviewByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<ReviewResponseDto>(
                "sp_GetReviewById",
                new
                {
                    ReviewId = id
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AddReviewAsync(AddReviewDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_AddReview",
                new
                {
                    dto.ProductId,
                    dto.UserId,
                    dto.Rating,
                    dto.ReviewText
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateReviewAsync(UpdateReviewDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_UpdateReview",
                new
                {
                    dto.ReviewId,
                    dto.Rating,
                    dto.ReviewText
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteReviewAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteReview",
                new
                {
                    ReviewId = id
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReviewResponseDto>> GetReviewsByProductIdAsync(int productId)
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<ReviewResponseDto>(
                "sp_GetReviewsByProductId",
                new
                {
                    ProductId = productId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}