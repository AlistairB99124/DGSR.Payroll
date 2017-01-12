using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Domain
{
    public interface IPayrollServiceDomain
    {

        Payroll.Domain.Entities.Payroll CommitToPayroll(DateTime date);
        List<PayrollItem> GetItemsByPayrollId(int ID);
    }
}
