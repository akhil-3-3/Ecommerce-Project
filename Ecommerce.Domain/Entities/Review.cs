namespace Ecommerce.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}