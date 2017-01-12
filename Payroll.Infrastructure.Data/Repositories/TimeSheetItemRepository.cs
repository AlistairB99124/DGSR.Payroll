using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System.Data.Entity;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class TimeSheetItemRepository : RepositoryBase<TimeSheetItem>, ITimeSheetItemRepository
    {
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public List<TimeSheetItem> GetByDate(DateTime date)
        {
            return _context.TimeSheetItems.Where(p => DbFunctions.TruncateTime(p.TimeIn) == DbFunctions.TruncateTime(date)).ToList();
        }

        public List<TimeSheetItem> GetByTimeSheetId(int id)
        {
            return _context.TimeSheetItems.Where(p => p.TimeSheetId == id).ToList();
        }

        public List<Employee> GetEmployeesLeft(DateTime Date)
        {
            var sheets = GetByDate(Date);
            var allEmployees = new List<Employee>();
            foreach(var p in _context.Employees.ToList())
            {
                allEmployees.Add(p);
            }
            foreach (var s in sheets)
            {
                foreach(var e in allEmployees.Reverse<Employee>())
                {
                    if (s.EmployeeId == e.Id)
                    {
                        allEmployees.Remove(e);
                    }
                }
            }
            return allEmployees;
            
        }
    }
}
