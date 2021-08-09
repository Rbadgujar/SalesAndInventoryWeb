using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class TaxReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: OIncomeItemReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.TaxReport("TaxReport", null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}