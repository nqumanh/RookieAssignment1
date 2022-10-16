namespace BookStore.Models
{
    public class Rating
    {
        public long Id { get; set; }

        public int Star { get; set; }

        public string? Comment { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
