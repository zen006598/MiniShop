using minishop.Models;

namespace minishop.Services.Interfaces;

public interface IShoppingCartService
{
    void AddItem(ShoppingCartItem id);
    ShoppingCartItem IncreaseAsync(int id);
    ShoppingCartItem DecreaseAsync(int id);
    int RemoveItemFromCartAsync(int id);
    List<ShoppingCartItem> GetCartItems();
    int GetCartAmount();
    Task<ShoppingCartItem> NewCartItemAsync(int productId);
}
