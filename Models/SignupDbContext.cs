using IdentityManagement.Models;
using Microsoft.EntityFrameworkCore;  
  
namespace IdentityManagement.Models
{  
    public class SignupDbContext : DbContext  
    {  
        public SignupDbContext(DbContextOptions<SignupDbContext> options) :  
            base(options)  
        {  
  
        }  
        public DbSet<SignupViewModel> AdminLogin{ get; set; }
        
    }  
}