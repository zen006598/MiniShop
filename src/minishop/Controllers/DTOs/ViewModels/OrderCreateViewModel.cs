using minishop.Models;

namespace minishop.Controllers.DTOs.ViewModels;

public class OrderCreateViewModel
{
    public string? Name { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverAddress { get; set; }
    public string? ReceiverPhone { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    public decimal Amount => Math.Ceiling(Math.Round(ShoppingCartItems.Sum(i => i.SubTotal), 2));
}
