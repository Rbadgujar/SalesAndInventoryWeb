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
		
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
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
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
                tbl_BankAdjustment objbank = new tbl_BankAdjustment();
                objbank.ListOfAccounts = (from obj in db.tbl_BankAccounts
                                          where obj.DeleteData.Equals(1)
                                          select new SelectListItem
                                          {
                                              Text = obj.BankName,
                                              Value = obj.ID.ToString()

                                          });
                return View(objbank);
            }
			else
			{

				var tb = db.tbl_BankAdjustmentselect("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new tbl_BankAdjustment();
				vm.BankAccount = tb.BankAccount;
				vm.EntryType = tb.EntryType;
				vm.Amount = tb.Amount;
				vm.Date = Convert.ToDateTime(tb.Date);
				vm.Description = tb.Description;
				return View(vm);
			}
		}

		[HttpPost]
		public ActionResult AddOrEdit(int id ,tbl_BankAdjustment conn)
		{
			if (id == 0)
			{

				db.tbl_BankAdjustmentselect("Insert", null, conn.BankAccount, conn.EntryType, conn.Amount, conn.Date, conn.Description, null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				db.tbl_BankAdjustmentselect("Update", id, conn.BankAccount, conn.EntryType, conn.Amount, conn.Date, conn.Description, null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
			
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