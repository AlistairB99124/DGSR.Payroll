using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IPayrollItemRepository:IRepositoryBase<PayrollItem>
    {
        List<PayrollItem> ReturnPayrollItems(int Payroll);

        List<PayrollItem> GetPayrollByBank(string Bank);


    }
}
