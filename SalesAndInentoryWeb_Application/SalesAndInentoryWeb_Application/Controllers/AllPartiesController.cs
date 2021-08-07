using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class AllPartiesController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: AllParties
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data(string date, string date2, string par) 
        {
            if (par == "0")
            {
                var tb1 = db.Reports("datetodate", null,null,null,null,null,null,null, date, null,null,null, null, null, null, null,null ,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null, date2, null, null,null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.Reports     ("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}