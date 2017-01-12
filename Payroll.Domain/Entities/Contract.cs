using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Contract
    {
        public int Id { get; set; }
        public bool Permanent { get; set; }
        public bool Contractor { get; set; }
        public bool Waged { get; set; }
        public bool Salaried { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Wage { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeId { get; set; }
        public int JobId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Job Job { get; set; }
    }
}
