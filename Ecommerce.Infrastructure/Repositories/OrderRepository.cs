using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : ConnectionRepository, IOrderRepository
    {
        public OrderRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<OrderResponseDto>(
                "sp_GetAllOrders",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<OrderResponseDto>(
                "sp_GetOrderById",
                new
                {
                    OrderId = id
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateOrderAsync(AddOrderDto dto)
        {
            using var connection = GetConnection();

            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                decimal totalAmount = dto.Items.Sum(item =>
                    (item.UnitPrice * item.Quantity) -
                    ((item.UnitPrice * item.Quantity) * item.Discount / 100));

                int orderId = await connection.ExecuteScalarAsync<int>(
                    "sp_AddOrder",
                    new
                    {
                        dto.UserId,
                        TotalAmount = totalAmount,
                        dto.ShippingAddress
                    },
                    transaction,
                    commandType: CommandType.StoredProcedure);

                foreach (var item in dto.Items)
                {
                    await connection.ExecuteAsync(
                        "sp_AddOrderDetail",
                        new
                        {
                            OrderId = orderId,
                            item.ProductId,
                            item.Quantity,
                            item.UnitPrice,
                            item.Discount
                        },
                        transaction,
                        commandType: CommandType.StoredProcedure);

                    await connection.ExecuteAsync(
                        "sp_UpdateStockAfterOrder",
                        new
                        {
                            item.ProductId,
                            item.Quantity
                        },
                        transaction,
                        commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();

                return orderId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<int> DeleteOrderAsync(int id)
        {
            using var connection = GetConnection();

            return await connection.ExecuteAsync(
                "sp_DeleteOrder",
                new
                {
                    OrderId = id
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}