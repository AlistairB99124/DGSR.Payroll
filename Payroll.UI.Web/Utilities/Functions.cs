using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Domain;
using Payroll.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.UI.Web.Utilities
{
    public static class Functions
    {
        public static List<TimeSheetSummaryModel> GetSummary(ITimeSheetServiceDomain timeSheetServiceDomain,List<TimeSheet> timeSheets)
        {
            List<TimeSheetSummaryModel> result = new List<TimeSheetSummaryModel>();
            List<TimeSheetItem> items = new List<TimeSheetItem>();
            foreach(var t in timeSheets)
            {
                var list = timeSheetServiceDomain.GetByTimeSheetId(t.Id);
                items.AddRange(list);
            }
            var emps = timeSheetServiceDomain.GetAllEmployees();
            foreach(var e in emps)
            {
                var empsItems = (from x in items where x.EmployeeId == e.Id select x).ToList();
                if (empsItems != null)
                {
                    Hours hours = GetHours(empsItems);
                    var timeSheetItem = new TimeSheetSummaryModel()
                    {
                        NormalHours = hours.Normal,
                        TimeOneThirdHours = hours.TimeOneThird,
                        TimeOneHalfHours = hours.TimeOneHalf,
                        DoubleTimeHours = hours.TimeDouble,
                        EffectiveHours = hours.EffectiveHours,
                        EmployeeName = e.FullName,
                        EmpId = e.EmpId
                    };
                    result.Add(timeSheetItem);
                }               
            }
            return result;
        }
        private static Hours GetHours(List<TimeSheetItem> sheets)
        {
            decimal normal = 0;
            decimal TimeOneThird = 0;
            decimal TimeOneHalf = 0;
            decimal TimeDouble = 0;
            foreach (var sheet in sheets)
            {
                if (sheet.TimeIn.DayOfWeek != DayOfWeek.Saturday && sheet.TimeIn.DayOfWeek != DayOfWeek.Sunday)
                {
                    decimal totalTime = Convert.ToDecimal((sheet.TimeOut - sheet.TimeIn).TotalHours);
                    if (totalTime > 9.0M)
                    {
                        normal += 9;
                        TimeOneThird += totalTime - 9;
                    }
                    else
                    {
                        normal += totalTime;
                    }
                }
                else
                {
                    decimal totalTime = Convert.ToDecimal((sheet.TimeOut - sheet.TimeIn).TotalHours);
                    if (sheet.TimeIn.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (totalTime > 5)
                        {
                            TimeOneHalf += 5;
                            TimeDouble += totalTime - 5;
                        }
                        else
                        {
                            TimeOneHalf += totalTime;
                        }
                    }
                    else
                    {
                        TimeDouble += totalTime;
                    }
                }
            }
            decimal Effective = normal + (1.333333M * TimeOneThird) + (1.5M * TimeOneHalf) + (2 * TimeDouble);
            Hours hours = new Hours()
            {
                Normal = normal,
                TimeOneThird = TimeOneThird,
                TimeOneHalf = TimeOneHalf,
                TimeDouble = TimeDouble,
                EffectiveHours = Effective
            };
            return hours;
        }
    }
    internal class Hours
    {
        public decimal Normal { get; set; }
        public decimal TimeOneThird { get; set; }
        public decimal TimeOneHalf { get; set; }
        public decimal TimeDouble { get; set; }
        public decimal EffectiveHours { get; set; }
    }
}
