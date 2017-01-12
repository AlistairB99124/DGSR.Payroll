using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IPayrollRepository:IRepositoryBase<Payroll.Domain.Entities.Payroll>
    {
        Entities.Payroll CommitToPayroll(DateTime Date);

        List<PayrollItem> GetItemsByPayrollId(int ID);
        
    }
}
