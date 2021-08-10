using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using SalesAndInentoryWeb_Application;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DayBookController : Controller
    {
        // GET: DayBook
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {         
            return View();
        }
        public ActionResult Daybook(string sortOrder, int? page, DateTime? datePicker)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            DateTime userSelectedDate = DateTime.Parse(Request["datePicker"]);

            var startDate = userSelectedDate.Date;


            var applications = from s in db.tbl_SaleInvoices
                               where s.InvoiceDate >= startDate
                               select s;

            return Json(new { data = applications }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Data(string date)
        {
            var tb = db.DayBookReport("Select", null, null, null, null, null, null, Convert.ToDateTime(date)).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}