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
			var tb = db.sp_CompanyBankAccount("Select1", null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.sp_CompanyBankAccount("Details", id, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.ID == id);
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

				var tb = db.sp_CompanyBankAccount("Details", id, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.ID == id);
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

				db.sp_CompanyBankAccount("Insert", null, conn.BankName, conn.AccountName,conn.AccountNo, conn.OpeningBal,Convert.ToString( conn.Date), Convert.ToInt32(Session["UserId"]));
				db.SubmitChanges();
                return RedirectToAction("Index");
			}
			else
			{
				db.sp_CompanyBankAccount("Update", id, conn.BankName, conn.AccountName,conn.AccountNo, conn.OpeningBal, (conn.Date).ToString(), Convert.ToInt32(Session["UserId"]));
				db.SubmitChanges();
                return RedirectToAction("Index");

            }
        }

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.sp_CompanyBankAccount("Delete", id, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}