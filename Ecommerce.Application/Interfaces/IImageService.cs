using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync();

        Task<ImageResponseDto?> GetImageByIdAsync(int id);

        Task<ImageResponseDto?> GetImageByProductIdAsync(int productId);

        Task<int> AddImageAsync(AddImageDto dto);

        Task<int> UpdateImageAsync(UpdateImageDto dto);

        Task<int> DeleteImageAsync(int id);
    }
}