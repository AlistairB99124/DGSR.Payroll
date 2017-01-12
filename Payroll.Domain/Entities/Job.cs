using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Job
    {
        public Job()
        {
            this.Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
