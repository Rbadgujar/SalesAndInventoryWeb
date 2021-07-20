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
        public ActionResult Index(string sortOrder, int? page, DateTime? datePicker)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            DateTime userSelectedDate = DateTime.Parse(Request["datePicker"]);

            var startDate = userSelectedDate.Date;
           

            var applications = from s in db.tbl_SaleInvoices
                               where s.InvoiceDate >= startDate
                               select s;

            switch (sortOrder)
            {
                default:
                    applications = applications.OrderByDescending(x => x.InvoiceDate);
                    break;
            }

           
            return View();
        }
    }
}