using Microsoft.EntityFrameworkCore;
using IdentityManagement.Models;

namespace IdentityManagement{
 public class EmployeeDataDbContext : DbContext  
    {  
        public EmployeeDataDbContext(DbContextOptions<EmployeeDataDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<EmployeeData> Employeess { get; set; }  
        
        public DbSet<Request> Requestss {get; set;}
    }  
}  
