namespace Ecommerce.Application.DTOs
{
    public class AddStockDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }

    public class UpdateStockDto
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }

    public class StockResponseDto
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}