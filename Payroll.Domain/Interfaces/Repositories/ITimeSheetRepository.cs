using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface ITimeSheetRepository:IRepositoryBase<TimeSheet>
    {
        void CommitDay(List<TimeSheetItem> items);
        List<TimeSheet> GetThisMonthsSheets(DateTime Date);
        Employee EmployeeByID(int ID);
    }
}
