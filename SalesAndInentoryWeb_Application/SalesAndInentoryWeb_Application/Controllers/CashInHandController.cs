using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CashInHandController : Controller
    {
		// GET: CashInHand
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		public ActionResult Index()
        {
            return View();

        }
        public ActionResult Data()
        {
            
				var tb = db.tbl_CashInhandSelect("Select1", null, null, null, null, null, null, null,null).ToList();
				return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_CashInhandSelect("Details", id, null, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
				return View(new tbl_CashInhand());
			}
			else
			{
				var tb = db.tbl_CashInhandSelect("Details", id, null, null, null, null, null, null, null).Single(x => x.ID == id);
				return View(tb);
			}
		}
		[HttpPost]
		public ActionResult AddOrEdit(int id, tbl_CashInhand emp)
		{

			if (emp.ID == 0)
			{
				db.tbl_CashInhandSelect("Insert", null, emp.Adjustment, emp.Amount, emp.Date, emp.Description,null,null,null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				db.tbl_CashInhandSelect("Update", id, emp.Adjustment, emp.Amount, emp.Date, emp.Description, null, null,null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_CashInhandSelect("Delete", id, null, null, null, null, null, null,null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
