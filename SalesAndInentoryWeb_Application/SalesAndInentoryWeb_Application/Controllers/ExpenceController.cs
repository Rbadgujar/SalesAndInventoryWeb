using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ExpenceController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: Expence
		public ActionResult Index()
        {
            return View();
        }

       [HttpGet]
       public ActionResult Expencescategory()
        {
            return View();
        }
		[HttpGet]
        public ActionResult ExpenceData()
        { 
			var tb = db.tbl_ExpensesSelect("Select1", null, null, null, null, null, null,null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);

		}

         
    }
}