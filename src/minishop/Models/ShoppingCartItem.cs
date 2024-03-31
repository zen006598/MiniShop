namespace minishop.Models;

public class ShoppingCartItem
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal SubTotal { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
}
