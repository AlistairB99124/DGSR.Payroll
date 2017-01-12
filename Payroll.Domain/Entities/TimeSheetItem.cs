using System;

namespace Payroll.Domain.Entities
{
    public partial class TimeSheetItem
    {
        public int Id { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> TimeSheetId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TimeSheet TimeSheet { get; set; }
    }
}
