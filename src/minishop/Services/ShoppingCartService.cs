using minishop.Models;
using minishop.Services.Interfaces;
using minishop.Commons.Helper;
using minishop.Services.DTOs.ResultModel;
using Microsoft.AspNetCore.Components.Forms;

namespace minishop.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly ISession _session;
    private readonly IProductService _product;

    private readonly ILogger<ShoppingCartService> _logger;

    public ShoppingCartService(
        IHttpContextAccessor httpContextAccessor,
        IProductService product,
        ILogger<ShoppingCartService> logger)
    {
        _session = httpContextAccessor.HttpContext.Session;
        _product = product;
        _logger = logger;
    }

    public void AddItem(ShoppingCartItem item)
    {
        List<ShoppingCartItem> items = GetItems();
        if (items is null)
        {
            items = new List<ShoppingCartItem> { item };
            SetCart(items);
        }
        else
        {
            int existItemIndex = items.FindIndex(m => m.ProductId.Equals(item.ProductId));

            if (existItemIndex != -1)
            {
                items[existItemIndex].Quantity += 1;
                items[existItemIndex].SubTotal += item.Price;
            }
            else
            {
                items.Add(item);
            }
            SetCart(items);
        }
    }

    public int RemoveItemFromCartAsync(int id)
    {
        //get the cart list from session
        List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(_session, "cart");
        if (cart is null)
        {
            _logger.LogError("Error: Can not get cart list from session : in the RemoveItemFromCartAsync method");
            return -1;
        }
        // look for item in cart
        int index = cart.FindIndex(m => m.ProductId.Equals(id));
        //is the item in cart remove it
        if (index != -1) cart.RemoveAt(index);
        //if the cart is empty remove cart from session
        if (cart.Count < 1)
        {
            SessionHelper.Remove(_session, "cart");
            return 0;
        }
        else
        {
            //reset cart in session
            SessionHelper.SetObjectAsJson(_session, "cart", cart);
            return id;
        }
    }

    public ShoppingCartItem DecreaseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public ShoppingCartItem IncreaseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public int GetCartAmount()
    {
        List<ShoppingCartItem> items = GetItems();
        if (items is null) return 0;
        int amount = (int)items.Sum(m => Math.Ceiling(m.Price) * m.Quantity);
        return amount;
    }

    public List<ShoppingCartItem> GetCartItems()
    {
        return GetItems();
    }

    public async Task<ShoppingCartItem> NewCartItemAsync(int id)
    {
        var product = await _product.GetAsync(id);
        if (product is null) return null;

        ShoppingCartItem item = new ShoppingCartItem
        {
            ProductName = product.Name,
            Quantity = 1,
            Price = product.Price,
            SubTotal = product.Price,
            ProductId = product.Id
        };

        return item;
    }

    private List<ShoppingCartItem> GetItems()
    {
        return SessionHelper.
            GetObjectFromJson<List<ShoppingCartItem>>(_session, "cart");
    }

    private void SetCart(List<ShoppingCartItem> items)
    {
        SessionHelper.SetObjectAsJson(_session, "cart", items);
    }
}
