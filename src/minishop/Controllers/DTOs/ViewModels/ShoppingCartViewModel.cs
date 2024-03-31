using minishop.Models;

namespace minishop.Controllers.DTOs.ViewModels;

public class ShoppingCartViewModel
{
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public int Amount { get; set; } = 0;
}
