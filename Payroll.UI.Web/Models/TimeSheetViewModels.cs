using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.UI.Web.Models
{
    public class TimeSheetSummaryModel
    {
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public decimal NormalHours { get; set; }
        public decimal TimeOneThirdHours { get; set; }
        public decimal TimeOneHalfHours { get; set; }
        public decimal DoubleTimeHours { get; set; }
        public decimal EffectiveHours { get; set; }
    }
}
