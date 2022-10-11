namespace BookStore.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public long CategoryId { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; }

        // public string Images { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
