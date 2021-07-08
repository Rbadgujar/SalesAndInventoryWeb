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
		[HttpGet]
        public ActionResult Data()
        {
            
				var tb = db.tbl_CashAdjustmentselect("Select1",  null, null, null, null, null, null,null).ToList();
				return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_CashAdjustmentselect("Details", id,  null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
				return View(new tbl_CashAdjustment());
			}
			else
			{
				var tb = db.tbl_CashAdjustmentselect("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
				
				var vm = new tbl_CashAdjustment();
				vm.CashAdjustment = tb.CashAdjustment;
				vm.CashAmount = tb.CashAmount;
				vm.Description = tb.Description;
				vm.BankName = tb.BankName;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
		}
		[HttpPost]
		public ActionResult AddOrEdit(int id, tbl_CashAdjustment emp)
		{

			if (id == 0)
			{
				db.tbl_CashAdjustmentselect("Insert", null, emp.CashAdjustment, emp.CashAmount,Convert.ToDateTime(emp.Date), emp.Description,emp.BankName,null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				db.tbl_CashAdjustmentselect("Update", id, emp.CashAdjustment, emp.CashAmount, Convert.ToDateTime(emp.Date), emp.Description, emp.BankName, null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_CashAdjustmentselect("Delete", id, null, null, null, null, null,null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
