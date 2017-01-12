using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface ITimeSheetItemRepository:IRepositoryBase<TimeSheetItem>
    {
        List<TimeSheetItem> GetByDate(DateTime date);
        List<Employee> GetEmployeesLeft(DateTime Date);
        List<Employee> GetAllEmployees();
        List<TimeSheetItem> GetByTimeSheetId(int id);      
    }
}
