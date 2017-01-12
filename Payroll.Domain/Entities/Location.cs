using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Location
    {
        public Location()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Postal_Code { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
