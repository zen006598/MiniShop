using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using minishop.Models;

namespace minishop.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<ShoppingCart>();
        builder.Ignore<ShoppingCartItem>();

        builder
            .Entity<Product>()
            .Property(p => p.Status)
            .HasConversion<int>();

        builder
            .Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");

        builder
            .Entity<Order>()
            .Property(p => p.ReceivedAmount)
            .HasColumnType("decimal(18, 2)");

        builder
            .Entity<Order>()
            .Property(p => p.Status)
            .HasConversion<int>();

        builder
            .Entity<OrderItem>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
    }
}
