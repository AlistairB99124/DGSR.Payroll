using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Payroll
    {
       public Payroll()
        {
            this.PayrollItems = new HashSet<PayrollItem>();
        }

        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

       public virtual ICollection<PayrollItem> PayrollItems { get; set; }
    }
}
