using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityManagement;
using IdentityManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace EmployeeDataController.Controllers
{
 
  
    public class EmployeeDataController : Controller
    {
        private readonly EmployeeDataDbContext database;
        private readonly IWebHostEnvironment _environment;
       

        public EmployeeDataController(EmployeeDataDbContext context, IWebHostEnvironment environment)
        {
            database = context;
            _environment = environment;
        }

       public async Task<IActionResult> viewProfile(){ 
        Console.WriteLine("user:"+Database.mail);
        var empolyees = await database.Employeess.Where(b=>b.UserID ==Database.mail).ToListAsync();
        return View(empolyees);
        }
        
  
        [HttpGet]
         [Authorize]
        [Route("EmployeeData/Index")]
        
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)

        {



            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null )
            {
                pageNumber = 1;
            }
           
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;


            var list = from selectedlist in database.Employeess
                     select selectedlist;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(selectedlist => selectedlist.UserID.Contains(searchString) || selectedlist.Name.Contains(searchString)
                                       || selectedlist.TeamName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.TeamName);
                    break;
                case "Date":
                    list = list.OrderBy(selectedlist => selectedlist.DateofBirth);
                    break;
                case "date_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.DateofBirth);
                    break;
                default:
                    list = list.OrderBy(selectedlist => selectedlist.DateofBirth);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<EmployeeData>.CreateAsync(list.AsNoTracking(), pageNumber ?? 1, pageSize));
             return View(await list.AsNoTracking().ToListAsync());
        }



     
        [Authorize]
        public async Task<IActionResult> Indexes(string sortOrder, string searchString, string currentFilter,int? pageNumber)

        {



            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

          
            var list = from selectedlist in database.Employeess
                     select selectedlist;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(selectedlist => selectedlist.UserID.Contains(searchString) || selectedlist.Name.Contains(searchString)
                                       || selectedlist.TeamName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.TeamName);
                    break;
                case "Date":
                    list = list.OrderBy(selectedlist => selectedlist.DateofBirth);
                    break;
                case "date_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.DateofBirth);
                    break;
                default:
                    list = list.OrderBy(selectedlist => selectedlist.DateofBirth);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedLists<EmployeeData>.CreateAsync(list.AsNoTracking(), pageNumber ?? 1, pageSize));
             return View(await list.AsNoTracking().ToListAsync());
        }


       
       
       
        [HttpGet]
        [Authorize]
        public IActionResult create()
        {

            EmployeeData employeeData = new EmployeeData();

            return View(employeeData);
        }

        [HttpPost]
       
        public async Task<IActionResult>  create(EmployeeData employeedata, IFormFile file)
        {
           if (ModelState.IsValid)
        {
            if (await database.Employeess.AnyAsync(x => x.UserID == employeedata.UserID))
            {
                ModelState.AddModelError("UserID", "Email ID already exists");
            }

            // Check if register number already exists
            if (await database.Employeess.AnyAsync(x => x.MobileNumber == employeedata.MobileNumber))
            {
                ModelState.AddModelError("MobileNumber", "MobileNumber number already exists");
            }
            if (await database.Employeess.AnyAsync(x => x.Name == employeedata.Name))
            {
                ModelState.AddModelError("Name", "Name already exists");
            }
            if (!ModelState.IsValid)
            {
                return View(employeedata);
            }
            
            EmployeeData employeeData = new EmployeeData();
            if (file == null || file.Length == 0)
            {
                employeeData.ProfilePicture = "NoImage.png";
            }
            else
            {
                string filename = System.Guid.NewGuid().ToString() + " .jpg";
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                employeeData.ProfilePicture = filename;

            }

        
            employeeData.Name=employeedata.Name;
            employeeData.UserID = employeedata.UserID;
            employeeData.Password = employeedata.Password;
            employeeData.Experience = employeedata.Experience;
            employeeData.EmployeePackage=employeedata.EmployeePackage;
            employeeData.Gender=employeedata.Gender;
            employeeData.MobileNumber = employeedata.MobileNumber;
            employeeData.Aadhaar=employeedata.Aadhaar;
            employeeData.Address = employeedata.Address;
            employeeData.TeamName = employeedata.TeamName;
            employeeData.DateofBirth = employeedata.DateofBirth;
             database.Employeess.Add(employeeData);
            await database.SaveChangesAsync();
            return RedirectToAction("Index");

        }
           return View(employeedata);
        }


         [HttpGet]

        public async Task<IActionResult> edit(int? id)

        {

            if (id == null)
            {
                return NotFound();
            }

            EmployeeData employeeData = await database.Employeess.Where(x => x.ID == id).FirstOrDefaultAsync();
            try{
            if (employeeData == null)
            {
                return NotFound();
            }
            return View(employeeData);
            }
             catch(Exception exception)
        {
         Console.WriteLine("The Error was Occured: " + exception.Message);
         return RedirectToAction("Index","Error");
        }
        }
        [HttpPost]
  
        public async Task<IActionResult> edit(int? id,EmployeeData employeedata, IFormFile iFormFile)

        {
           if (id == null)
            {
                return NotFound();

            }



            EmployeeData employeeData = await database.Employeess.Where(x => x.ID == id).FirstOrDefaultAsync();



            if (employeeData == null)

            {

                return NotFound();

            }





            if (iFormFile != null )

            {



                string filename = System.Guid.NewGuid().ToString() + " .jpg";

                var path = Path.Combine(

                            Directory.GetCurrentDirectory(), "wwwroot", "images", filename);



                using (var stream = new FileStream(path, FileMode.Create))

                {

                    await iFormFile.CopyToAsync(stream);

                }



                employeeData.ProfilePicture = filename;



            }
            employeeData.Name=employeedata.Name;
            employeeData.UserID = employeedata.UserID;
            employeeData.Password = employeedata.Password;
            employeeData.Experience = employeedata.Experience;
            employeeData.EmployeePackage=employeedata.EmployeePackage;
            employeeData.Gender=employeedata.Gender;
            employeeData.MobileNumber = employeedata.MobileNumber;
            employeeData.Aadhaar=employeedata.Aadhaar;
            employeeData.Address = employeedata.Address;
            employeeData.TeamName = employeedata.TeamName;
            employeeData.DateofBirth = employeedata.DateofBirth;
            
                await database.SaveChangesAsync();
                return RedirectToAction("Index");    

        }



       
          [HttpGet("viewDetails/{id:int}")]
         
        public async Task<IActionResult> viewDetails(int? id)


        {

            if (id == null)

            {

                return NotFound();

            }



            EmployeeData employeeData = await database.Employeess.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (employeeData == null)

            {

                return NotFound();

            }



            return View(employeeData);

        }



//   [HttpGet]
          [HttpGet("viewDetail/{id:int}")]

        public async Task<IActionResult> viewDetail(int? id)


        {

            if (id == null)

            {

                return NotFound();

            }



            EmployeeData employeeData = await database.Employeess.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (employeeData == null)

            {

                return NotFound();

            }



            return View(employeeData);

        }

        [HttpGet]
        public async Task<IActionResult> delete(int? id)
        {
            if (id == null || database.Employeess == null)
            {
                return NotFound();
            }

            var Employeelist = await database.Employeess
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Employeelist == null)
            {
                return NotFound();
            }

            return View(Employeelist );
        }

        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (database.Employeess == null)
            {
                return Problem("Entity set 'BookDbContext.Book'  is null.");
            }
            var EmployeeDatas = await database.Employeess.FindAsync(id);
            if (EmployeeDatas != null)
            {
                database.Employeess.Remove(EmployeeDatas);
            }
            
            await database.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // private bool BookExists(int id)
        // {
        //   return (database.Employeess?.Any(e => e.ID == id)).GetValueOrDefault();
        // }





     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
 
    }
}