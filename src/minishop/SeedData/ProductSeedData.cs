using Bogus;
using Microsoft.EntityFrameworkCore;
using minishop.Commons;
using minishop.Data;
using minishop.Models;

namespace minishop.SeedData;

public static class ProductSeedData
{
    public static void Initialize(IServiceProvider serviceProvider, string adminUserId)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (!context.Products.Any())
            {
                var productFaker = new Faker<Product>()
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20))
                        .RuleFor(p => p.Price, f => Math.Ceiling(f.Random.Decimal(1, 9999)))
                        .RuleFor(p => p.CreateAt, f => DateTime.UtcNow)
                        .RuleFor(p => p.CreateBy, "system")
                        .RuleFor(p => p.Status, ProductStatus.Active)
                        .RuleFor(p => p.UserId, adminUserId);

                var productFakers = productFaker.Generate(10);
                context.Products.AddRange(productFakers);
                context.SaveChanges();
            }
        }
    }
}
