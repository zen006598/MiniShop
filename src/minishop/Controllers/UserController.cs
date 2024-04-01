using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using minishop.Controllers.DTOs.ViewModels;
using minishop.Models;
using minishop.Services.DTOs.ResultModel;
using minishop.Services.Interfaces;

namespace minishop.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderService _order;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(
            ILogger<UserController> logger,
            IMapper mapper,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _order = orderService;
            _userManager = userManager;
        }

        [HttpGet("Orders"), Authorize]
        public async Task<IActionResult> Orders()
        {
            string userId = _userManager.GetUserId(User);
            var orders = await _order.ListOrdersWithUserAsync(userId);
            if (orders is null) return NotFound();
            var orderViewModel = _mapper.Map<
                   IEnumerable<OrderResultModel>,
                   IEnumerable<OrderViewModel>>(orders);
            return View(orderViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}