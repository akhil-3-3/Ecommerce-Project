namespace Ecommerce.Application.DTOs
{
    public class AddBrandDto
    {
        public string BrandName { get; set; } = string.Empty;
    }

    public class UpdateBrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }

    public class BrandResponseDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}