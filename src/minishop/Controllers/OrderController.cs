using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using minishop.Controllers.DTOs.Parameters;
using minishop.Controllers.DTOs.ViewModels;
using minishop.Models;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;
using minishop.Services.Interfaces;

namespace minishop.Controllers;

[Route("[controller]"), Authorize]
public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _order;
    private readonly IShoppingCartService _shoppingCart;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(
        ILogger<OrderController> logger,
        IMapper mapper,
        IOrderService orderService,
        IShoppingCartService shoppingCartService,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _order = orderService;
        _shoppingCart = shoppingCartService;
        _userManager = userManager;
    }
    [HttpGet("Index")]
    public async Task<IActionResult> Index(OrderParameter parameter)
    {
        var info = _mapper.Map<
            OrderParameter,
            OrderSearchInfo>(parameter);

        var orders = await _order.SearchOrdersAsync(info);

        var result = _mapper.Map<
            IEnumerable<OrderResultModel>,
            IEnumerable<OrderViewModel>>(orders);
        return View(result);
    }
    [HttpGet("Checkout")]
    public IActionResult New()
    {
        List<ShoppingCartItem> cartItems = _shoppingCart.GetCartItems();

        if (cartItems is null)
        {
            return RedirectToAction("Index", "ShoppingCart");
        }

        OrderCreateViewModel orderCreateViewModel = new OrderCreateViewModel()
        {
            ShoppingCartItems = cartItems
        };

        return View(orderCreateViewModel);
    }
    [HttpPost("Checkout"), ActionName("New"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateParameter orderToCreate)
    {
        List<ShoppingCartItem> cartItems = _shoppingCart.GetCartItems();
        if (!ModelState.IsValid) goto ERROR;

        var orderInfo = _mapper.Map<OrderInfo>(orderToCreate);

        var user = _userManager.GetUserAsync(User).Result;

        string orderId = await _order.Insert(orderInfo, user);

        if (orderId != "") return RedirectToAction("Details", "Order", new { id = orderId });

        ERROR:
        OrderCreateViewModel orderViewModel = _mapper.Map<OrderCreateViewModel>(orderToCreate);
        orderViewModel.ShoppingCartItems = cartItems;
        return View(orderToCreate);
    }
    [HttpGet("Details")]
    public async Task<IActionResult> Details(string id)
    {
        string userId = _userManager.GetUserId(User);
        var order = await _order.GetOrderWithUserAsync(Guid.Parse(id), userId);
        if (order is null) return NotFound();
        OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);
        return View(orderViewModel);
    }
    [HttpGet("Cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        bool isSuccess = await _order.Cancel(id);
        if (isSuccess) return RedirectToAction("Details", "Order", new { id = id });
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
