using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PartyStatementController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.PartyStatement("Select", null, null, null,null,null,null, Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
    }
}