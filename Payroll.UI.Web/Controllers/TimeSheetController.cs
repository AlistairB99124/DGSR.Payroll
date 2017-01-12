using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Domain;
using Payroll.Domain.Interfaces.Repositories;
using Payroll.UI.Web.Models;
using Payroll.UI.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML;
using ClosedXML.Excel;
using System.Data.Entity;

namespace Payroll.UI.Web.Controllers
{
    [Authorize(Roles ="Clerk")]
    public class TimeSheetController : Controller
    {
        private ITimeSheetServiceDomain _timeSheetServiceDomain;

        public TimeSheetController(ITimeSheetServiceDomain timeSheetServiceDomain)
        {
            _timeSheetServiceDomain = timeSheetServiceDomain;
        }
        // GET: TimeSheet
        public ActionResult Index(string Date)
        {
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
            var employees = _timeSheetServiceDomain.GetAllEmployees();
            ViewBag.Date = date;
            return View(employees);
        }

        public ActionResult Create(string date)
        {
            List<Employee> employeesLeft;
            List<TimeSheetItem> items;
            SelectList empList;
            DateTime theDate;
            bool Committed = false;
            if (date == null)
            {
                theDate = DateTime.Now;
                employeesLeft = _timeSheetServiceDomain.GetEmployeesLeft(theDate);
                items = _timeSheetServiceDomain.GetByDate(theDate);
                if (items.Count == 0)
                {
                    Committed = false;
                }
                else
                {
                    if (items.FirstOrDefault().TimeSheetId != null)
                    {
                        Committed = true;
                    }
                }
            }
            else
            {
                int day = Convert.ToInt32(date.Split('.')[0]);
                int month = Convert.ToInt32(date.Split('.')[1]);
                int year = Convert.ToInt32(date.Split('.')[2]);
                theDate = new DateTime(year, month, day);
                employeesLeft = _timeSheetServiceDomain.GetEmployeesLeft(theDate);
                items = _timeSheetServiceDomain.GetByDate(theDate);
                if (items.Count == 0)
                {
                    Committed = false;
                }
                else
                {
                    if (items.FirstOrDefault().TimeSheetId != null)
                    {
                        Committed = true;
                    }
                }
               
            }
            if (employeesLeft == null)
            {
                empList = null;
            }
            else
            {
                empList = new SelectList(employeesLeft,"Id","FullName","1");
            }
            ViewBag.AllItems = _timeSheetServiceDomain.GetAllItems().OrderBy(p => p.TimeIn);
            ViewBag.EmployeeList = empList;
            ViewBag.Committed = Committed;
            ViewBag.Today = theDate;
            return View(items);
        }
        

        [HttpPost]
        public ActionResult AddItem(FormCollection collection)
        {
            var date = collection.Get("dateItem");
            var empId = Convert.ToInt32(collection.Get("EmployeeList"));
            var timeIn = collection.Get("timeIn");
            var timeOut = collection.Get("timeOut");
            int hourIn = 0;
            int minIn = 0;
            int hourOut = 0;
            int minOut = 0;
            var timeInSplit = timeIn.Split(new char[] { ':', ' ' });
            var timeOutSplit = timeOut.Split(new char[] { ':', ' ' });
            if (timeInSplit[2]== "PM")
            {
                hourIn = Convert.ToInt32(timeInSplit[0]) + 12;
                minIn = Convert.ToInt32(timeInSplit[1]);
            }
            else
            {
                hourIn = Convert.ToInt32(timeInSplit[0]);
                minIn = Convert.ToInt32(timeInSplit[1]);
            }
            if (timeOutSplit[2] == "PM")
            {
                hourOut = Convert.ToInt32(timeOutSplit[0])+12;
                minOut = Convert.ToInt32(timeOutSplit[1]);
            }
            else
            {
                hourOut = Convert.ToInt32(timeOutSplit[0]);
                minOut = Convert.ToInt32(timeInSplit[1]);
            }
            var day = Convert.ToInt32(date.Split('.')[0]);
            var month = Convert.ToInt32(date.Split('.')[1]);
            var year = Convert.ToInt32(date.Split('.')[2]);
            DateTime dateTimeIn = new DateTime(year,month,day,hourIn,minIn,0);
            DateTime dateTimeOut = new DateTime(year, month, day, hourOut, minOut, 0);

            var timesheetItem = new TimeSheetItem()
            {
                EmployeeId = empId,
                TimeIn = dateTimeIn,
                TimeOut = dateTimeOut
            };
            _timeSheetServiceDomain.InsertSheetItem(timesheetItem);
            return RedirectToAction("Create", "TimeSheet", new { date = date });
        }

        [HttpPost]
        public ActionResult DeleteItem(FormCollection collection)
        {
            int itemId = Convert.ToInt32(collection.Get("itemId"));
            string date = collection.Get("date");
            _timeSheetServiceDomain.DeleteItem(itemId);
            return RedirectToAction("Create","TimeSheet",new { date = date });
        }

        [HttpPost]
        public ActionResult CommitDay(FormCollection collection)
        {
            string Date = collection.Get("date");
            int day = Convert.ToInt32(Date.Split('.')[0]);
            int month = Convert.ToInt32(Date.Split('.')[1]);
            int year = Convert.ToInt32(Date.Split('.')[2]);

            DateTime theDate = new DateTime(year, month, day);

            List<TimeSheetItem> items = _timeSheetServiceDomain.GetByDate(theDate);
            _timeSheetServiceDomain.CommitDay(items);
            return RedirectToAction("Create", "TimeSheet", new { date = Date });
        }

        [HttpGet]
        public ActionResult MonthSummary(string Date)
        {
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
            var thisMonthsSheets = _timeSheetServiceDomain.GetMonthsSheets(date);
            List<TimeSheetSummaryModel> summaryModel = Functions.GetSummary(_timeSheetServiceDomain, thisMonthsSheets);
            ViewBag.Date = date;
            return View(summaryModel);
        }

        [HttpPost]
        public ActionResult ExportHoursToExcel(FormCollection collection)
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
            var thisMonthsSheets = _timeSheetServiceDomain.GetMonthsSheets(date);
            List<TimeSheetSummaryModel> summaryModel = Functions.GetSummary(_timeSheetServiceDomain, thisMonthsSheets);

            var data = new DataTable();
            data.Columns.Add("EmpID", typeof(string));
            data.Columns.Add("Employee", typeof(string));
            data.Columns.Add("Normal", typeof(decimal));
            data.Columns.Add("TimeOneThird", typeof(decimal));
            data.Columns.Add("TimeOneHalf", typeof(decimal));
            data.Columns.Add("TimeDouble", typeof(decimal));
            data.Columns.Add("Effective", typeof(decimal));

            foreach(var x in summaryModel)
            {
                data.Rows.Add(x.EmpId, x.EmployeeName, x.NormalHours, x.TimeOneThirdHours, x.TimeOneHalfHours, x.DoubleTimeHours, x.EffectiveHours);
            }
            data.TableName = "Hours";
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(data);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Hours_" + date.ToString("MMM_yyyy") +  ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("MonthSummary", "TimeSheet", date);
        }

        [HttpGet]
        public ActionResult EmployeeDetails(int ID, string Date)
        {
            Employee employee = _timeSheetServiceDomain.GetEmployeeById(ID);
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
            ViewBag.Employee = employee;
            ViewBag.Date = date;
            List<TimeSheetItem> items = _timeSheetServiceDomain.GetAllItems().Where(t => t.EmployeeId == employee.Id && t.TimeIn.Month==date.Month && t.TimeIn.Year==date.Year).ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult EmployeeTimeDetails(int empId, int timeId)
        {
            var timesheetItems = _timeSheetServiceDomain.GetByTimeSheetId(timeId);
            var timeSheetItem = timesheetItems.Where(x => x.EmployeeId == empId).FirstOrDefault();
            var employee = _timeSheetServiceDomain.GetEmployeeById(empId);

            ViewBag.Date = timeSheetItem.TimeIn;
            ViewBag.Employee = employee;
            return View(timeSheetItem);
        }
    }
}