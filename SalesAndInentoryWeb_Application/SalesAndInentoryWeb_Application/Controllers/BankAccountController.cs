using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class BankAccountController : Controller
	{

		CompanyDataClassDataContext db = new CompanyDataClassDataContext();


		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Data()
		{
			var tb = db.BankAccountSelect("Select", null, null, null, null, null,null, MainLoginController.companyid1).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.BankAccountSelect("Details", id, null, null, null, null, null, null).Single(x =>x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{ 
			if (id == 0)
			{
				return View(new tbl_BankAccount());
			}
			else
			{
				
				var tb = db.BankAccountSelect("Details", id,null,null,null,null,null,null).Single(x => x.ID == id);
				var vm = new tbl_BankAccount();
				vm.AccountName = tb.AccountName;
				vm.BankName = tb.BankName;
				vm.AccountNo = tb.AccountNo;
				vm.OpeningBal = tb.OpeningBal;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
		}

		[HttpPost]
		public ActionResult AddOrEdit(int id, tbl_BankAccount conn)
		{
			if (id == 0)
			{

				db.BankAccountSelect("Insert", null, conn.AccountName, conn.BankName, conn.AccountNo, conn.OpeningBal, conn.Date, MainLoginController.companyid1);
				db.SubmitChanges();
                ViewBag.meg = "Stored Suucessfully";
                return View("Index");
   //             return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
			else
			{
				db.BankAccountSelect("Update", id, conn.AccountName, conn.BankName, conn.AccountNo, conn.OpeningBal, conn.Date, MainLoginController.companyid1);
				db.SubmitChanges();
				return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.BankAccountSelect("Delete", id, null,null,null,null,null,null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}

