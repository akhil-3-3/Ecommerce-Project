using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();

        Task<OrderResponseDto?> GetOrderByIdAsync(int id);

        Task<int> CreateOrderAsync(AddOrderDto dto);

        Task<int> DeleteOrderAsync(int id);
    }
}