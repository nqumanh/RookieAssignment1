namespace CustomerSite.Models
{
    public class Order
    {
        public long Id { get; set; }

        public virtual User? User { get; set; }

        public virtual List<OrderLine>? OrderLines { get; set; }
    }
}
