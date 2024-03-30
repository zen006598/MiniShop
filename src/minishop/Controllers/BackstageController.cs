using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace minishop.Controllers;

[Route("[controller]"), Authorize(Roles = "Admin")]
public class BackstageController : Controller
{
    private readonly ILogger<BackstageController> _logger;

    public BackstageController(ILogger<BackstageController> logger)
    {
        _logger = logger;
    }
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
