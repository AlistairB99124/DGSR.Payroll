using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using ClosedXML;
using ClosedXML.Excel;
using Payroll.Domain.Interfaces.Domain;

namespace Payroll.UI.Web.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        private IPayrollServiceDomain _payrollRepository;

        public PayrollController(IPayrollServiceDomain payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }
        
        [HttpPost]
        public ActionResult CommitToPayroll(FormCollection collection)
        {
            string Date = collection.Get("date");
            DateTime date;
            if (Date == null)
            {
                date = DateTime.Now;
            }
            else
            {
                int day = Convert.ToInt32(Date.Split('.')[0]);
                int month = Convert.ToInt32(Date.Split('.')[1]);
                int year = Convert.ToInt32(Date.Split('.')[2]);
                date = new DateTime(year, month, day);
            }
            Payroll.Domain.Entities.Payroll Payroll = _payrollRepository.CommitToPayroll(date);
            return RedirectToAction("Index", new { id = Payroll.Id });
        }

        // GET: Payroll
        [HttpGet]
        public ActionResult Index(int id)
        {
            var items = _payrollRepository.GetItemsByPayrollId(id);
            DateTime date = new DateTime(items.FirstOrDefault().Payroll.Year, items.FirstOrDefault().Payroll.Month,1);
            ViewBag.Date = date;
            return View(items);
        }

        [HttpPost]
        public ActionResult ExportPayrollToExcel(FormCollection collection)
        {
            int payrollId = Convert.ToInt32(collection.Get("payrollId"));
            var items = _payrollRepository.GetItemsByPayrollId(payrollId);

            var date = new DateTime(items.FirstOrDefault().Payroll.Year, items.FirstOrDefault().Payroll.Month, 1);

            var dt = new DataTable();
            dt.Columns.Add("Emp ID", typeof(string));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("Normal", typeof(decimal));
            dt.Columns.Add("Overtime (x1.33)", typeof(decimal));
            dt.Columns.Add("Overtime (x1.5)", typeof(decimal));
            dt.Columns.Add("Overtime (x2)", typeof(decimal));
            dt.Columns.Add("Effective Hours", typeof(decimal));
            dt.Columns.Add("Rate (R/Hr)", typeof(decimal));
            dt.Columns.Add("Amount (Gross)", typeof(decimal));
            dt.Columns.Add("Amount (Taxable)", typeof(decimal));            
            dt.Columns.Add("Bonus", typeof(decimal));
            dt.Columns.Add("Gross Wages", typeof(decimal));
            dt.Columns.Add("G/Tax", typeof(decimal));
            dt.Columns.Add("PAYE", typeof(decimal));
            dt.Columns.Add("SNPF Employee", typeof(decimal));
            dt.Columns.Add("SNPF Employer", typeof(decimal));
            dt.Columns.Add("Amount (Net)", typeof(decimal));
            dt.Columns.Add("Loan (Deduct)", typeof(decimal));
            dt.Columns.Add("Advance (Add)", typeof(decimal));
            dt.Columns.Add("Amount Paid", typeof(decimal));

            foreach(var x in items)
            {
                var contract = x.Employee.Contracts.Where(t => t.ExpiryDate > DateTime.Now).FirstOrDefault();
                if (contract.Waged)
                {
                    dt.Rows.Add(
                    x.Employee.EmpId,
                    x.Employee.FullName,
                    x.NormalHours,
                    x.TimeOneThirdHours,
                    x.TimeOneHalfHours,
                    x.DoubleTimeHours,
                    x.EffectiveHours,
                    contract.Wage,
                    x.AmountGross,
                    x.AmountTaxable,
                    x.Bonus,
                    x.Gross_Wage,
                    x.General_Tax,
                    x.PAYE,
                    x.SNPF_Employee,
                    x.SNPF_Employer,
                    x.Amount_Net,
                    x.Loan,
                    x.Advance,
                    x.Amount_Paid
                    );
                }
                else
                {
                    dt.Rows.Add(
                   x.Employee.EmpId,
                   x.Employee.FullName,
                   0,
                   0,
                   0,
                   0,
                   0,
                   contract.Salary,
                   x.AmountGross,
                   x.AmountTaxable,
                   x.Bonus,
                   x.Gross_Wage,
                   x.General_Tax,
                   x.PAYE,
                   x.SNPF_Employee,
                   x.SNPF_Employer,
                   x.Amount_Net,
                   x.Loan,
                   x.Advance,
                   x.Amount_Paid
                   );
                }
                
            }
            dt.TableName = "Payroll";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Payroll_" + date.ToString("MMM_yyyy") + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }


            return RedirectToAction("Index", "Payroll", new { id = 1 });
        }
    }
}