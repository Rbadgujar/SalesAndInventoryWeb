using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class OtherIncomeReportController : Controller
	{
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: OtherIncomeReport
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Data()
		{
			var tb = db.tbl_OtherIncomeSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}


		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_OtherIncomeSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);

		}
	}
}