namespace minishop.Models;

public class ShoppingCart
{
    public int UserId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
}
