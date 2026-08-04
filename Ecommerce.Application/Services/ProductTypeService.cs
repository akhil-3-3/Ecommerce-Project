using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<IEnumerable<ProductTypeResponseDto>> GetAllProductTypesAsync()
        {
            return await _productTypeRepository.GetAllProductTypesAsync();
        }

        public async Task<ProductTypeResponseDto?> GetProductTypeByIdAsync(int id)
        {
            return await _productTypeRepository.GetProductTypeByIdAsync(id);
        }

        public async Task<int> AddProductTypeAsync(AddProductTypeDto dto)
        {
            return await _productTypeRepository.AddProductTypeAsync(dto);
        }

        public async Task<int> UpdateProductTypeAsync(UpdateProductTypeDto dto)
        {
            return await _productTypeRepository.UpdateProductTypeAsync(dto);
        }

        public async Task<int> DeleteProductTypeAsync(int id)
        {
            return await _productTypeRepository.DeleteProductTypeAsync(id);
        }
    }
}