using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();

        Task<OrderResponseDto?> GetOrderByIdAsync(int id);

        Task<int> CreateOrderAsync(AddOrderDto dto);

        Task<int> DeleteOrderAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId);
        Task<int> CancelOrderAsync(int orderId);
    }
}