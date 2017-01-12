using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            string q = collection.Get("q");
            return RedirectToAction("Search", new { query = q });            
        }

        [HttpGet]
        public ActionResult Search(string query)
        {
            string[] stringArray = query.Split(new char[] { ' ', ',', '-' });
            List<string> Keywords = stringArray.ToList();
            return View();
        }
    }
}