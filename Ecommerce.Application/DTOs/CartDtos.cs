using Ecommerce.Application.DTOs;

public class AddCartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CartItemResponseDto
{
    public int CartItemId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }
}