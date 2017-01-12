using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Domain
{
    public interface ITimeSheetServiceDomain
    {
        void CommitDay(List<TimeSheetItem> items);
        List<TimeSheetItem> GetByDate(DateTime date);
        List<Employee> GetEmployeesLeft(DateTime Date);
        List<Employee> GetAllEmployees();
        void InsertSheetItem(TimeSheetItem item);
        void DeleteItem(int itemId);
        List<TimeSheetItem> GetAllItems();
        List<TimeSheet> GetMonthsSheets(DateTime Date);
        List<TimeSheetItem> GetByTimeSheetId(int id);
        Employee GetEmployeeById(int ID);
    }
}
