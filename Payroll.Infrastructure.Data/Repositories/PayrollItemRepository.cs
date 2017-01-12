using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using Payroll.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class PayrollItemRepository : RepositoryBase<PayrollItem>, IPayrollItemRepository
    {
        public List<PayrollItem> ReturnPayrollItems(int Payroll)
        {
            throw new NotImplementedException();
        }

        public List<PayrollItem> GetPayrollByBank(string Bank)
        {
            return _context.PayrollItems.Where(x => x.Employee.Bank.Name == Bank).ToList();
        }
    }
}
