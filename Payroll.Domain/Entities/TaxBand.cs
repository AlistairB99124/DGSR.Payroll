using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public class TaxBand
    {
        public double MarginalRate { get; set; }
        public double BaseAmount { get; set; }
        public double Threshold { get; set; }
        public double Limit { get; set; }
    }
}
