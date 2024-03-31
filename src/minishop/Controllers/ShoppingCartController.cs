using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minishop.Controllers.DTOs.ViewModels;
using minishop.Models;
using minishop.Services.Interfaces;

namespace minishop.Controllers
{
    [Route("[controller]"), Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartController(
            ILogger<ShoppingCartController> logger,
            IShoppingCartService shoppingCartService
        )
        {
            _logger = logger;
            _shoppingCart = shoppingCartService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            ShoppingCartViewModel cart = new ShoppingCartViewModel()
            {
                Items = _shoppingCart.GetCartItems() ?? new List<ShoppingCartItem>(),
                Amount = _shoppingCart.GetCartAmount()
            };
            return View(cart);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}