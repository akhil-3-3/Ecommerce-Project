using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.DTOs
{
    public class AddImageDto
    {
        public int ProductId { get; set; }
        
        public List<IFormFile>? ImageFiles { get; set; }
    }

    public class ImageDto
    {
        public int ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string ImagePublicId { get; set; } = string.Empty;
    }

    public class UpdateImageDto
    {
        public int ImageId { get; set; }

        public int ProductId { get; set; }
        
        public IFormFile? ImageFile { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string ImagePublicId { get; set; } = string.Empty;
    }

    public class ImageResponseDto
    {
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string ImagePublicId { get; set; } = string.Empty;
    }
}