using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityManagement;
using IdentityManagement.Models;
using System.Diagnostics;

namespace RequestController.Controllers
{
    public class RequestController : Controller
    {
        private readonly EmployeeDataDbContext db;
        private readonly IWebHostEnvironment _environment;
        
        public RequestController(EmployeeDataDbContext context, IWebHostEnvironment environment)
        {
            db = context;
            _environment = environment;
        }
        
        public async Task<IActionResult> requestStatus(){ 
      
        var userRequest = await db.Requestss.Where(b=>b.UserId ==Database.mail).ToListAsync();
        return View(userRequest);
        }
  
        [HttpGet]

        public async Task<IActionResult> viewRequestList(string sortOrder, string searchString, string currentFilter, int? pageNumber)

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


            var list = from selectedlist in db.Requestss
                     select selectedlist;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(selectedlist => selectedlist.UserId.Contains(searchString) || selectedlist.Name.Contains(searchString)
                                       );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.Name);
                    break;
                case "Date":
                    list = list.OrderBy(selectedlist => selectedlist.UserId);
                    break;
                case "date_desc":
                    list = list.OrderByDescending(selectedlist => selectedlist.UserId);
                    break;
                default:
                    list = list.OrderBy(selectedlist => selectedlist.UserId);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Request>.CreateAsync(list.AsNoTracking(), pageNumber ?? 1, pageSize));
             return View(await list.AsNoTracking().ToListAsync());
        }



       
        [HttpGet]
        public IActionResult create()
        {
            Request request = new Request();

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult>  create(Request employeeRequest, IFormFile file)
        {
            Request request = new Request();
            request.Name=Database.Name;
            request.UserId = Database.mail;
            request.request = employeeRequest.request;
            request.ExistingData=employeeRequest.ExistingData;
            request.NewData=employeeRequest.NewData;
            request.status="Pending";
            db.Requestss.Add(request);
            await db.SaveChangesAsync();
            return RedirectToAction("Create");

        }

         [HttpGet]

        public async Task<IActionResult> edit(int? id)

        {

            if (id == null)
            {
                return NotFound();
            }

            Request request = await db.Requestss.Where(x => x.ID == id).FirstOrDefaultAsync();

            if (request == null)
            {
                return NotFound();
            }
            return View(request);

        }
        [HttpPost]

        public async Task<IActionResult> edit(int? id,Request employeerequest, IFormFile file)

        {
           if (id == null)
            {
                return NotFound();

            }


            Request request = await db.Requestss.Where(x => x.ID == id).FirstOrDefaultAsync();



            if (request == null)

            {

                return NotFound();

            }

            
             request.status=employeerequest.status;
            
                await db.SaveChangesAsync();
                return RedirectToAction("viewRequestList");    

        }



       

  [HttpGet]


        public async Task<IActionResult> viewDetail(int? id)


        {

            if (id == null)

            {

                return NotFound();

            }



            Request request = await db.Requestss.Where(x => x.ID == id).FirstOrDefaultAsync();





            if (request == null)

            {

                return NotFound();

            }



            return View(request);

        }

  [HttpGet]
        public async Task<IActionResult> delete(int? id)
        {
            if (id == null || db.Requestss == null)
            {
                return NotFound();
            }

            var Requestlist = await db.Requestss
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Requestlist == null)
            {
                return NotFound();
            }

            return View(Requestlist );
        }

        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleteConfirmed(int id)
        {
            if (db.Requestss == null)
            {
                return Problem("Entity set 'EmployeeDataDbContext.Requestss'  is null.");
            }
            var EmployeeDatas = await db.Requestss.FindAsync(id);
            if (EmployeeDatas != null)
            {
                db.Requestss.Remove(EmployeeDatas);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(viewRequestList));
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
 
    }
}