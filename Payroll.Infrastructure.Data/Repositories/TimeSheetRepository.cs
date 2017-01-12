using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class TimeSheetRepository : RepositoryBase<TimeSheet>, ITimeSheetRepository
    {
        public void CommitDay(List<TimeSheetItem> items)
        {
            var sheet = new TimeSheet() { Day = items.FirstOrDefault().TimeIn };
            _context.TimeSheets.Add(sheet);
            _context.SaveChanges();
            int timeId = 0;
            while (timeId == 0)
            {
                timeId = _context.TimeSheets.Where(p => p.Day == sheet.Day).FirstOrDefault().Id;
            }
            items.ForEach(t => t.TimeSheetId = timeId);
        }

        public Employee EmployeeByID(int ID)
        {
            return _context.Employees.Find(ID);
        }

        public List<TimeSheet> GetThisMonthsSheets(DateTime Date)
        {
            List<DateTime> Dates = Enumerable.Range(1, DateTime.DaysInMonth(Date.Year, Date.Month))
                .Select(day => new DateTime(Date.Year, Date.Month, day))
                .ToList();
            List<TimeSheet> monthsSheets = new List<TimeSheet>();
            foreach(var day in Dates)
            {
                var sheet = _context.TimeSheets.Where(p => DbFunctions.TruncateTime(p.Day) == DbFunctions.TruncateTime(day)).FirstOrDefault();
                if (sheet != null)
                {
                    monthsSheets.Add(sheet);
                }
            }
            return monthsSheets;

        }
    }
}
