using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using minishop.Services.Interfaces;

namespace minishop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartApiController : ControllerBase
{

    private readonly ILogger<ShoppingCartController> _logger;
    private readonly IShoppingCartService _shoppingCart;
    private readonly IMapper _mapper;
    private readonly IProductService _product;
    public ShoppingCartApiController(
        ILogger<ShoppingCartController> logger,
        IShoppingCartService shoppingCartService,
        IMapper mapper,
        IProductService productService
    )
    {
        _logger = logger;
        _shoppingCart = shoppingCartService;
        _mapper = mapper;
        _product = productService;
    }

    [HttpGet("AddToCart")]
    public async Task<IActionResult> AddToCart(int id)
    {
        var item = await _shoppingCart.NewCartItemAsync(id);
        if (item is null) return NotFound();
        _shoppingCart.AddItem(item);
        return Ok(new
        {
            status = "success",
            message = $"{item.ProductName} added to cart"
        });
    }

    [HttpGet("RemoveItemFromCart")]
    public async Task<IActionResult> RemoveItemFromCart(int id)
    {
        int result = _shoppingCart.RemoveItemFromCartAsync(id);
        string status;
        string message;

        switch (result)
        {
            case -1:
                status = "error";
                message = "Item not found.";
                break;
            case 0:
                status = "success";
                message = "The cart is empty.";
                break;
            default:
                var product = await _product.GetAsync(id);
                status = "success";
                message = $"{product.Name} removed from cart.";
                break;
        }

        return Ok(new { status, message });
    }
}
