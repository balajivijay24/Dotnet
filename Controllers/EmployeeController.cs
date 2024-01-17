using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using IdentityManagement.Filters;
using Serilog;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;



namespace IdentityManagement.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public static string? password { get; private set; }
    public static string? Mail { get; private set; }

     private readonly EmployeeDataDbContext db;
    private readonly IWebHostEnvironment _environment;
       

        public EmployeeController(EmployeeDataDbContext context, IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }
   
     [Authorize]
    public new IActionResult request()
    {
       
        return View();
    }
     public new IActionResult viewProfile()
    {
       
        return View();
    }
    public new IActionResult viewData()
    {
       
        return View();
    }
   

   
    [HttpGet]
    [CustomResultFilter]
    public IActionResult employeeLogin()
    {
        ClaimsPrincipal claimUser=HttpContext.User;
        if(claimUser.Identity.IsAuthenticated){
               
            return RedirectToAction("viewProfile","EmployeeData");
        }
        TempData["password"]=password;
        return View();
    }
 
    [HttpPost]
 
     public async Task<IActionResult> employeeLoginAsync(User user ,Database database)
    {
      
       if (await db.Employeess.AnyAsync(x => x.UserID == user.EmailId))
        {
           
       string result= Database.employeeLogin(user);
       Console.WriteLine(result);
       if(result=="Employee")
       {
            List<Claim> claims=new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier,user.EmailId)
        };
        ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties properties=new AuthenticationProperties(){
        AllowRefresh=true,
        IsPersistent=database.KeepLoggedIn
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),properties);
          Log.Information("Employee Login Triggered");  
         HttpContext.Session.SetString("EmailId",Database.displayEmployee(user.EmailId));
          return RedirectToAction("viewProfile","EmployeeData",user);

       }
        TempData["msg"]="Login Id or password is incorrect";
       return View("employeeLogin",user);
    } 

        
    else 
        {

        string result= Database.adminLogin(user);
        Console.WriteLine(result);

       if(result=="Admin"){
        List<Claim> claims=new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier,user.EmailId)
        };
        ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties properties=new AuthenticationProperties(){
            AllowRefresh=true,
            IsPersistent=database.KeepLoggedIn
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),properties);
         Log.Information("Employee Login Triggered");

          HttpContext.Session.SetString("EmailId",Database.display(user.EmailId));
          return RedirectToAction("viewProfile",user);
       }
         TempData["msg"]="Login Id or password is incorrect";
         return View("employeeLogin",user);
        }

       TempData["msg"]="Login Id or password is incorrect";
       return View("employeeLogin",user);
   
    }


///forget password///
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
            return RedirectToAction("checkCode","Employee",employee);
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
            return RedirectToAction("EmployeeLogin","Employee");
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
