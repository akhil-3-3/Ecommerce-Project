namespace Ecommerce.Application.DTOs
{
    public class AddWishlistDto
    {
        public int ProductId { get; set; }
    }


    public class WishlistResponseDto
    {
        public int WishlistItemId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public List<ImageResponseDto> Images { get; set; } = new();
    }
}
