using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankAdjustmentController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: BankAdjustment
		[HttpGet]
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult adjustdata()
		{
			var tb = db.tbl_BankAdjustmentselect("Select1", null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_BankAdjustmentselect("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult InsertCustomer()
		{
			return View();
		}

		[HttpPost]
		public JsonResult AddOrEdit(tbl_BankAdjustment conn)
		{
			db.tbl_BankAdjustmentselect("Insert", null, conn.BankAccount, conn.EntryType, conn.Amount, conn.Date, conn.Description, null);
			db.SubmitChanges();
			return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_BankAdjustmentselect("Delete", id, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}