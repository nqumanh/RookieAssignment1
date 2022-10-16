namespace CustomerSite.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public float Price { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual List<OrderLine>? OrderLines { get; set; }

        public virtual List<Category> Categories { get; set; } = new List<Category>();

        public virtual List<Rating>? Ratings { get; set; }
    }
}
