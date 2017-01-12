using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.UI.Web.Models
{
    public class CashScheduleViewModel
    {
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public decimal WageDue { get; set; }
        public int OneHundredNote { get; set; }
        public int FiftyNote { get; set; }
        public int TwentyNote { get; set; }
        public int TenNote { get; set; }
        public int FiveCoin { get; set; }
        public int TwoCoin { get; set; }
        public int OneCoin { get; set; }
    }

    public class Totals
    {
        public int Hundreds { get; set; }
        public int Fifties { get; set; }
        public int Twenties { get; set; }
        public int Tens { get; set; }
        public int Fives { get; set; }
        public int Twos { get; set; }
        public int Ones { get; set; }
        public decimal Wages { get; set; }
    }
}
