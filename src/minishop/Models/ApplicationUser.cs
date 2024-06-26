using Microsoft.AspNetCore.Identity;

namespace minishop.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Product>? Products { get; set; }
    public ICollection<Order>? Orders { get; set; }
}