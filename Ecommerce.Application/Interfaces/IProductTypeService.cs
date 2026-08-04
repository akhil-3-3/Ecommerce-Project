using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductTypeResponseDto>> GetAllProductTypesAsync();

        Task<ProductTypeResponseDto?> GetProductTypeByIdAsync(int id);

        Task<int> AddProductTypeAsync(AddProductTypeDto dto);

        Task<int> UpdateProductTypeAsync(UpdateProductTypeDto dto);

        Task<int> DeleteProductTypeAsync(int id);
    }
}