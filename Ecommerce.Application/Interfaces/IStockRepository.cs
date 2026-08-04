using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockResponseDto>> GetAllStockAsync();

        Task<StockResponseDto?> GetStockByProductIdAsync(int productId);

        Task<int> AddStockAsync(AddStockDto dto);

        Task<int> UpdateStockAsync(UpdateStockDto dto);

        Task<int> DeleteStockAsync(int stockId);
    }
}