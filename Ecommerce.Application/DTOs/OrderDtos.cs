namespace Ecommerce.Application.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }

    public class AddOrderDto
    {
        public int UserId { get; set; }

        public string ShippingAddress { get; set; } = string.Empty;

        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }
    }
}