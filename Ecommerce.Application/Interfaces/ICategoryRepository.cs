using Ecommerce.Application.DTOs;


namespace Ecommerce.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryResponseDto>>GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
        Task<int> AddCategoryAsync(AddCategoryDto dto);
        Task<int> UpdateCategoryAsync(UpdateCategoryDto dto);
        Task<int> DeleteCategoryAsync(int id);

    }
}
