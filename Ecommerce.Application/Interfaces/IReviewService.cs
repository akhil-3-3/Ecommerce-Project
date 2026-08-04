using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync();

        Task<ReviewResponseDto?> GetReviewByIdAsync(int id);

        Task<int> AddReviewAsync(AddReviewDto dto);

        Task<int> UpdateReviewAsync(UpdateReviewDto dto);

        Task<int> DeleteReviewAsync(int id);
    }
}