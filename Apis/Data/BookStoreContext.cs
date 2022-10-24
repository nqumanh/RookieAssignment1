using Apis.Models;
using Microsoft.EntityFrameworkCore;

namespace Apis.Data;

public class BookStoreContext : DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderLine>? OrderLines { get; set; }
}