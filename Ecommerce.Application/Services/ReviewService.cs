using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<ReviewResponseDto?> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetReviewByIdAsync(id);
        }

        public async Task<int> AddReviewAsync(AddReviewDto dto)
        {
            return await _reviewRepository.AddReviewAsync(dto);
        }

        public async Task<int> UpdateReviewAsync(UpdateReviewDto dto)
        {
            return await _reviewRepository.UpdateReviewAsync(dto);
        }

        public async Task<int> DeleteReviewAsync(int id)
        {
            return await _reviewRepository.DeleteReviewAsync(id);
        }
        public async Task<IEnumerable<ReviewResponseDto>> GetReviewsByProductIdAsync(int productId)
        {
            return await _reviewRepository.GetReviewsByProductIdAsync(productId);
        }
    }
}