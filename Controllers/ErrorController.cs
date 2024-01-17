using Microsoft.AspNetCore.Mvc;
namespace IdentityManagement.Controllers{
    public class ErrorController : Controller{
        public IActionResult viewIndexError(){
            return View();
        }

         public IActionResult viewWebApiError(){
            return View();
        }
    }
}