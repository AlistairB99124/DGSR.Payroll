using Payroll.Domain.Entities;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository:IRepositoryBase<Employee>
    {
        Employee RecoverEmployeeByEmail(string email);
    }
}
