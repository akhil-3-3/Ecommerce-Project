namespace Ecommerce.Application.DTOs
{
    public class AddReviewDto
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? ReviewText { get; set; }
    }

    public class UpdateReviewDto
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string? ReviewText { get; set; }
    }

    public class ReviewResponseDto
    {
        public int ReviewId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string? ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}