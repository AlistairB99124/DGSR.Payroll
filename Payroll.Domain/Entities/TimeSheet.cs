using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public class TimeSheet
    {
        public TimeSheet()
        {
            this.TimeSheetItems = new HashSet<TimeSheetItem>();
        }

        public int Id { get; set; }
        public DateTime Day { get; set; }

        public virtual ICollection<TimeSheetItem> TimeSheetItems { get; set; }
    }
}
