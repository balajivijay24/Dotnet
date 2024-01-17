using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using IdentityManagement.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace IdentityManagement.Controllers;
[ExceptionLogFilter]
public class HomeController : Controller
{
   
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
     [LogActionFilter]
     
    public IActionResult Index()
    {
        return View("Index");
    }

    public async Task<IActionResult> logOut()
    {  
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Console.WriteLine(User.Identity.IsAuthenticated);
        return RedirectToAction("Index","Home");
    }

   
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
