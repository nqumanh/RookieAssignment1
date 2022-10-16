namespace BookStore.Models
{
    public class OrderLine
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Product? Product { get; set; }
    }
}
