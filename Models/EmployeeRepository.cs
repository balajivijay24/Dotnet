using IdentityManagement;
using IdentityManagement.Models;
using System.Linq;

public class EmployeeRepository
{
    private EmployeeDataDbContext _context;

    public EmployeeRepository(EmployeeDataDbContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeData> getEmployees()
    {
        return _context.Employeess;
    }

    
}
