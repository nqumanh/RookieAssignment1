using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
    }
}