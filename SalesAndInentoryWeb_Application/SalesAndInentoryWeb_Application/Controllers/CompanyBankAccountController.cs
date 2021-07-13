using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CompanyBankAccountController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: CompanyBankAccount
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
        public ActionResult Data()
        {
			var tb = db.sp_CompanyBanckAccount("Select1", null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.sp_CompanyBanckAccount("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
				return View(new CompanyBankAccount());
			}
			else
			{

				var tb = db.sp_CompanyBanckAccount("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new CompanyBankAccount();
				vm.AccountName = tb.AccountName;
				vm.BankName = tb.BankName;
				vm.AccountNo = tb.AccountNo;
				vm.OpeningBal = tb.OpeningBal;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
		}

		[HttpPost]
		public ActionResult AddOrEdit(int id, CompanyBankAccount conn)
		{
			if (id == 0)
			{

				db.sp_CompanyBanckAccount("Insert", null, conn.BankName, conn.AccountName,Convert.ToInt32(conn.AccountNo), conn.OpeningBal, (conn.Date).ToString(), null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				db.sp_CompanyBanckAccount("Update", id, conn.BankName, conn.AccountName, Convert.ToInt32(conn.AccountNo), conn.OpeningBal, (conn.Date).ToString(), null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.sp_CompanyBanckAccount("Delete", id, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}