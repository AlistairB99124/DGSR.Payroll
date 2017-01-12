using Payroll.Domain.Interfaces.Domain;
using Payroll.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.UI.Web.Controllers
{
    public class ReportController : Controller
    {

        private IReportServiceDomain _reportServiceDomain;

        public ReportController(IReportServiceDomain reportServiceDomain)
        {
            _reportServiceDomain = reportServiceDomain;
        }

        // GET: Report
        public ActionResult CashSchedule(int PayrollId)
        {
            var items = _reportServiceDomain.GetPayrollByBank("CASH").Where(x => x.PayrollId == PayrollId).ToList();

            List<CashScheduleViewModel> view = new List<CashScheduleViewModel>();
            foreach(var item in items)
            {
                int hundreds = 0;
                int fifties = 0;
                int twenties = 0;
                int tens = 0;
                int fives = 0;
                int twos = 0;
                int ones = 0;
                decimal remaining = Math.Ceiling(item.Amount_Paid);
                while (remaining >= 100)
                {
                    ++hundreds;
                    remaining = remaining - 100;
                }
                while (remaining >= 50)
                {
                    ++fifties;
                    remaining = remaining - 50;
                }
                while (remaining >= 20)
                {
                    ++twenties;
                    remaining = remaining - 20;
                }
                while(remaining >= 10)
                {
                    ++tens;
                    remaining = remaining - 10;
                }
                while (remaining >= 5)
                {
                    ++fives;
                    remaining = remaining - 5;
                }
                while (remaining >= 2)
                {
                    ++twos;
                    remaining = remaining - 2;
                }
                while (remaining >= 1)
                {
                    ++ones;
                    remaining = remaining - 1;
                }
                var viewItem = new CashScheduleViewModel
                {
                    OneHundredNote = hundreds,
                    FiftyNote = fifties,
                    TwentyNote = twenties,
                    TenNote = tens,
                    FiveCoin = fives,
                    TwoCoin = twos,
                    OneCoin = ones,
                    EmpCode = item.Employee.EmpId,
                    Name = item.Employee.FullName,
                    WageDue = Math.Ceiling(item.Amount_Paid)                     
                };
                view.Add(viewItem);
            }

            var totals = new Totals
            {
                Hundreds = view.Sum(x => x.OneHundredNote) * 100,
                Fifties = view.Sum(x => x.FiftyNote) * 50,
                Twenties = view.Sum(x => x.TwentyNote) * 20,
                Tens = view.Sum(x => x.TenNote) * 10,
                Fives = view.Sum(x => x.FiveCoin) * 5,
                Twos = view.Sum(x => x.TwentyNote) * 2,
                Ones = view.Sum(x => x.OneCoin) * 1,
                Wages = view.Sum(x => x.WageDue)
            };
            IEnumerable<CashScheduleViewModel> viewResult = view.OrderByDescending(x=>x.EmpCode) as IEnumerable<CashScheduleViewModel>;
            ViewBag.Totals = totals;
            return View(viewResult);
        }
    }

    
}