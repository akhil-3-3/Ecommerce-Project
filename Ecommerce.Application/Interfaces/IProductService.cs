using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDtos>> GetAllProductsAsync();

        Task<ProductResponseDtos?> GetProductByIdAsync(int id);

        Task<int> AddProductAsync(AddProductDtos dto);

        Task<int> UpdateProductAsync(UpdateProductDtos dto);

        Task<int> DeleteProductAsync(int id);
    }
}