using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System.Data.Entity;
using Payroll.Infrastructure.Data.Context;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class PayrollRepository : RepositoryBase<Payroll.Domain.Entities.Payroll>, IPayrollRepository
    {
        public Payroll.Domain.Entities.Payroll CommitToPayroll(DateTime date)
        {
            List<TimeSheet> sheets = GetThisMonthsSheets(_context, date);
            // Create the collection that will store the Payroll Items
            List<PayrollItem> payrollItems = new List<PayrollItem>();
            // Create the collection that will store the time sheet 
            // items from the time sheet in the parameter
            List<TimeSheetItem> timeItems = new List<TimeSheetItem>();
            // Create, Add and Save Changes for the Payroll instance 
            // that will be assocaited with the payroll items to be 
            // created.
            Payroll.Domain.Entities.Payroll payroll = new Domain.Entities.Payroll()
            {
                Month = sheets.FirstOrDefault().Day.Month,
                Year = sheets.FirstOrDefault().Day.Year
            };
            _context.Payrolls.Add(payroll);
            _context.SaveChanges();

            // To ensure that the database has been updated correctly
            // use while loop to make sure the Id has been created.
            // Use a DateTime instance to time out if this takes to long
            int payrollId = 0;
            DateTime StartOfLoop = DateTime.Now;
            while (payrollId == 0)
            {
                payrollId = _context.Payrolls
                    .Where(p => p.Month == payroll.Month && 
                    p.Year == payroll.Year)
                    .FirstOrDefault().Id;
                DateTime endOfIteration = DateTime.Now;
                TimeSpan span = StartOfLoop - endOfIteration;
                if (span.Seconds > 10)
                {
                    throw new ApplicationException("Database took too long to respond");
                }
            }
            // Fill Time Sheet Item collection from Time Sheet Ids
            foreach(var s in sheets)
            {
                var list = _context.TimeSheetItems.Where(t => t.TimeSheetId == s.Id);
                timeItems.AddRange(list);
            }
            // Get List of all employees
            var emps = _context.Employees.ToList();
            
            // Loop through each employee to determine their payroll for the month
            foreach(var e in emps)
            {
                // Get Time Sheet Items that have the employee's Id
                var empsItems = (from x in timeItems where x.EmployeeId == e.Id select x).ToList();
                // Check if the employee did any work this month
                if (empsItems != null)
                {
                    // Get summary of hours from Time Sheet Items
                    Hours theHours = new Hours();
                    Hours hours = theHours.GetHours(empsItems);
                    // Create Payroll Item with non-dependent fields
                    var payrollItem = new PayrollItem(e,payroll,hours);
                    //Add to collection
                    payrollItems.Add(payrollItem);
                }
            }
            // Add range to Database
            _context.PayrollItems.AddRange(payrollItems);

            // Return the payroll
            return _context.Payrolls.Find(payrollId);
        }
        public static List<TimeSheet> GetThisMonthsSheets(ContextBank _context, DateTime Date)
        {
            List<DateTime> Dates = Enumerable.Range(1, DateTime.DaysInMonth(Date.Year, Date.Month))
               .Select(day => new DateTime(Date.Year, Date.Month, day))
               .ToList();
            List<TimeSheet> monthsSheets = new List<TimeSheet>();
            foreach (var day in Dates)
            {
                var sheet = _context.TimeSheets.Where(p => DbFunctions.TruncateTime(p.Day) == DbFunctions.TruncateTime(day)).FirstOrDefault();
                if (sheet != null)
                {
                    monthsSheets.Add(sheet);
                }
            }
            return monthsSheets;
        }

        public List<PayrollItem> GetItemsByPayrollId(int ID)
        {
            return _context.PayrollItems.Where(p => p.PayrollId == ID).ToList();
        }
    }
}
