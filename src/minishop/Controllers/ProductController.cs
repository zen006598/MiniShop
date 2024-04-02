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

[Route("[controller]"), Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _product;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductController(
        ILogger<ProductController> logger,
        IMapper mapper,
        IProductService product,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _product = product;
        _userManager = userManager;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var products = await _product.AllAsync();
        var productsViewModel = _mapper.Map<IEnumerable<ProductResultModel>, IEnumerable<ProductViewModel>>(products);
        return View(productsViewModel);
    }


    [HttpGet("New")]
    public IActionResult New()
    {
        ProductViewModel product = new();
        return View(product);
    }

    [HttpPost("Create"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductParameter parameter)
    {
        if (!ModelState.IsValid) return View(parameter);
        var info = _mapper.Map<ProductParameter, ProductInfo>(parameter);
        var user = _userManager.GetUserAsync(User).Result;
        var isSuccess = await _product.Insert(info, user);
        _logger.LogInformation("Insert new product. Is success: {isSuccess}", isSuccess);
        if (isSuccess)
        {
            return RedirectToAction("Index", "Backstage");
        }
        return View(parameter);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _product.GetAsync(id);
        var productViewModel = _mapper.Map<ProductResultModel, ProductViewModel>(product);
        return View(productViewModel);
    }

    [HttpPost("Update/{id}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ProductParameter parameter)
    {
        if (!ModelState.IsValid) return View(parameter);

        var product = await _product.GetAsync(id);
        if (product is null) return NotFound();
        ProductInfo productInfo = _mapper.Map<ProductParameter, ProductInfo>(parameter);
        bool isSuccess = _product.Update(id, productInfo);
        if (isSuccess) return RedirectToAction("Index", "Backstage");
        return View(parameter);
    }

    [HttpGet("Details/{id}"), AllowAnonymous]
    public async Task<IActionResult> Detail(int id)
    {
        var product = await _product.GetAsync(id);
        if (product is null) return NotFound();
        var productViewModel = _mapper.Map<ProductResultModel, ProductViewModel>(product);
        return View(productViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
