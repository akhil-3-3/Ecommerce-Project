using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageResponseDto>> GetAllImagesAsync();

        Task<ImageResponseDto?> GetImageByIdAsync(int id);

        Task<ImageResponseDto?> GetImageByProductIdAsync(int productId);

        Task<int> AddImageAsync(ImageDto dto);

        Task<int> UpdateImageAsync(UpdateImageDto dto);

        Task<int> DeleteImageAsync(int id);
    }
}