using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Web;
using static System.Console;
using Microsoft.AspNetCore.Http;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace IdentityManagement.Controllers;

public class AdminController : Controller
{
  

    public static string? password { get; private set; }
    public static string? Mail { get; private set; }

    [Authorize]
       public IActionResult viewProfile()
    {
       
        return View();
    }
    
  
    [HttpGet]
    public IActionResult forgotPassword()
    {
         return View();
    }


    [HttpPost]
    public IActionResult forgotPassword(User employee)
    {   
      string mail=Database.sendEmail(employee.EmailId,employee);
      Mail=employee.EmailId;
        if (mail=="sent")
        {
            ViewBag.OTP="Verification Code sent successfuly \n Enter otp to continue";
            return RedirectToAction("checkCode","Admin",employee);
        }
        else
        {
            ViewBag.Message="Your are not a registered user";
            return View("forgotPassword");
        }
    }

    [HttpGet]
    public IActionResult checkCode()
    {
         return View();
    }

    public IActionResult checkCode(Database database)
    {  
      string code=Database.verifyCode(database);
        if (code=="code sent")
        { 
            password=Database.sendPassword(Mail);
            return RedirectToAction("AdminLogin","Admin");
        }
        else
        {
            ViewBag.OTP="Incorrect Code";
            return View("checkCode",database);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
