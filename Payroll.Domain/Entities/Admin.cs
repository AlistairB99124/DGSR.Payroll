using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
