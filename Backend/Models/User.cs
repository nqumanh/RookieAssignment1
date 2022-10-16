using BookStore.Models.Enums;

namespace BookStore.Models
{
    public class User
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public UserRole Role { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}
