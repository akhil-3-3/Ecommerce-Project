using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(AddOrderDto dto)
        {
            dto.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var orderId = await _orderService.CreateOrderAsync(dto);

            return Ok(new
            {
                Message = "Order placed successfully.",
                OrderId = orderId
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var rows = await _orderService.DeleteOrderAsync(id);

            if (rows == 0)
                return NotFound();

            return Ok("Order deleted successfully.");
        }
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var orders = await _orderService.GetMyOrdersAsync(userId);

            return Ok(orders);
        }
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var rows = await _orderService.CancelOrderAsync(id);

            if (rows == 0)
                return BadRequest("Order cannot be cancelled.");

            return Ok("Order cancelled successfully.");
        }
    }
}