using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ItemTrackingReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

         [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.ItemTrackingReport("ItemTrackingReport", null, null, null, null,null,null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}