using Microsoft.AspNetCore.Http;
namespace Ecommerce.Application.DTOs
{
    public class AddProductDtos
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? VolumeML { get; set; }

        public string Gender { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal Discount { get; set; }

    }

    public class UpdateProductDtos
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? VolumeML { get; set; }

        public string Gender { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImagePublicId { get; set; }
    }
    public class ProductResponseDtos
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;

        public string? Description { get; set; }
        public int? VolumeMl { get; set; }

        public decimal Rating { get; set; }

        public string Gender { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? Updated { get; set; }

        public List<ImageResponseDto> Images { get; set; } = new();
    }
}
