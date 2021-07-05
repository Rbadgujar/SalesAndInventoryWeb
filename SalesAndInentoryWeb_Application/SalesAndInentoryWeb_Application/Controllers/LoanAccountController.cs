using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class LoanAccountController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: LoanAccount
		public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public ActionResult BankData()
		{
			var tb = db.tbl_LoanBankSelect("Select1", null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Detail(int id)
		{
			var tb = db.tbl_LoanBankSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
        {
			if (id == 0)
			{
				return View(new tbl_LoanBank());
			}
			else
			{
				var tb = db.tbl_LoanBankSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
				return View(tb);
			}
        }
		[HttpPost]
		public ActionResult AddOrEdit(int id, tbl_LoanBank emp)
		{
			if (ModelState.IsValid)
			{
				db.tbl_LoanBankSelect("Insert", null, emp.AccountName, emp.AccountNo, emp.Description, emp.LendarBank, emp.FirmName, emp.CurrentBal, emp.BalAsOf, emp.LoanReceive, emp.Interest, emp.Duration, emp.ProcessingFees, emp.PaidBy, null, null, emp.Total);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				db.tbl_LoanBankSelect("Update", id, emp.AccountName, emp.AccountNo, emp.Description, emp.LendarBank, emp.FirmName, emp.CurrentBal, emp.BalAsOf, emp.LoanReceive, emp.Interest, emp.Duration, emp.ProcessingFees, emp.PaidBy, null, null, emp.Total);
				db.SubmitChanges();
				return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

			[HttpPost]
		  public ActionResult Delete(int id)
		  {
			  var tb = db.tbl_LoanBankSelect("Delete", id, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null).ToList();
			  db.SubmitChanges();
			  return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		  }
	}
}