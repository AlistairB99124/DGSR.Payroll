using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.UI.Web.Controllers
{
    [Authorize]
    public class PayrollItemController : Controller
    {
        private IPayrollItemServiceDomain _serviceDomain;

        public PayrollItemController(IPayrollItemServiceDomain serviceDomain)
        {
            _serviceDomain = serviceDomain;
        }

        // GET: PayrollItem
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePayroll(FormCollection collection)
        {
            string bonusString = collection.Get("bonusVal");
            string loanString = collection.Get("loanVal");
            string advanceString = collection.Get("advanceVal");
            decimal bonus = Convert.ToDecimal(bonusString,CultureInfo.InvariantCulture);
            decimal loan = Convert.ToDecimal(loanString, CultureInfo.InvariantCulture);
            decimal advance = Convert.ToDecimal(advanceString, CultureInfo.InvariantCulture);
            int payrollItemId = Convert.ToInt32(collection.Get("payrollVal"));
            var item = _serviceDomain.GetItem(payrollItemId);
            item.Loan = loan;
            item.Advance = advance;
            item.Bonus = bonus;
            _serviceDomain.UpdateItem(item);

            return RedirectToAction("Index", "Payroll", new { id = item.PayrollId });
        }       
    }
    internal class TaxBand
    {
        public double MarginalRate { get; set; }
        public double BaseAmount { get; set; }
        public double Threshold { get; set; }
        public double Limit { get; set; }
    }
}