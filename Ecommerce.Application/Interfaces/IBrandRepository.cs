using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync();

        Task<BrandResponseDto?> GetBrandByIdAsync(int id);

        Task<int> AddBrandAsync(AddBrandDto dto);

        Task<int> UpdateBrandAsync(UpdateBrandDto dto);

        Task<int> DeleteBrandAsync(int id);
    }
}