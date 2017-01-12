using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System.Linq;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public Employee RecoverEmployeeByEmail(string email)
        {
            var employee = _context.Employees.Where(c => c.Email == email).FirstOrDefault();
            return employee;
        }
    }
}
