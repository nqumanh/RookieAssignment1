using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Apis.Models;
using Microsoft.EntityFrameworkCore;

namespace Apis.Data;

public class BookStoreContext : IdentityDbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderLine>? OrderLines { get; set; }
    public DbSet<Rating>? Ratings { get; set; }
}