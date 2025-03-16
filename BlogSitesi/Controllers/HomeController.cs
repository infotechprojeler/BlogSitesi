using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogSitesi.Models;
using BlogSitesi.Data;

namespace BlogSitesi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _databaseContext;

    public HomeController(ILogger<HomeController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public IActionResult Index()
    {
        return View(_databaseContext.Posts.Where(p => p.IsActive).Take(3));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
