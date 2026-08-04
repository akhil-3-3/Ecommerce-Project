using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<StockResponseDto>> GetAllStockAsync()
        {
            return await _stockRepository.GetAllStockAsync();
        }

        public async Task<StockResponseDto?> GetStockByProductIdAsync(int productId)
        {
            return await _stockRepository.GetStockByProductIdAsync(productId);
        }

        public async Task<int> AddStockAsync(AddStockDto dto)
        {
            return await _stockRepository.AddStockAsync(dto);
        }

        public async Task<int> UpdateStockAsync(UpdateStockDto dto)
        {
            return await _stockRepository.UpdateStockAsync(dto);
        }

        public async Task<int> DeleteStockAsync(int productId)
        {
            return await _stockRepository.DeleteStockAsync(productId);
        }
    }
}