namespace BookStore.Models
{
    public class Rating
    {
        public long ProductId { get; set; }

        public string? CustomerId { get; set; }

        public int Star { get; set; }

        public string? comment { get; set; }
    }
}
