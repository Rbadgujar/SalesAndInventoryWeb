using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class OIncomeCategoryReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: OIncomeCategoryReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.OtherIncome("OtherSelect", null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}