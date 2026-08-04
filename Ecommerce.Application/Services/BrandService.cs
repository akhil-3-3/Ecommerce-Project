using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

        public async Task<BrandResponseDto?> GetBrandByIdAsync(int id)
        {
            return await _brandRepository.GetBrandByIdAsync(id);
        }

        public async Task<int> AddBrandAsync(AddBrandDto dto)
        {
            return await _brandRepository.AddBrandAsync(dto);
        }

        public async Task<int> UpdateBrandAsync(UpdateBrandDto dto)
        {
            return await _brandRepository.UpdateBrandAsync(dto);
        }

        public async Task<int> DeleteBrandAsync(int id)
        {
            return await _brandRepository.DeleteBrandAsync(id);
        }
    }
}