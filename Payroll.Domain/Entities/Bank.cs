using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Bank
    {
        public Bank()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Branch_Code { get; set; }
        public string Account_No { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
