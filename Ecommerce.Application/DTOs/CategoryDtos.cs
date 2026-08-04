

namespace Ecommerce.Application.DTOs
{
    public class AddCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
    }
    public class  UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
    public class  CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }



}

