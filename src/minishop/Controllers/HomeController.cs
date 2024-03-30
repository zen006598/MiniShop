using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minishop.Commons;
using minishop.Controllers.DTOs.ViewModels;
using minishop.Models;
using minishop.Services.DTOs.ResultModel;
using minishop.Services.Interfaces;

namespace minishop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _product;

    public HomeController(
        ILogger<HomeController> logger,
        IMapper mapper,
        IProductService productService)
    {
        _logger = logger;
        _mapper = mapper;
        _product = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _product.GetProductsByStatusAsync(ProductStatus.Active);
        var productsViewModel = _mapper.Map<IEnumerable<ProductResultModel>, IEnumerable<ProductViewModel>>(products);
        return View(productsViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
