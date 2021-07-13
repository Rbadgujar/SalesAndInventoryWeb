using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class MakePaymentController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: MakePayment
        
		public ActionResult Makepayment()
		{
			return View();
		}
		[HttpGet]
        public ActionResult makykdata()
        {
            var getdata = db.tbl_MakePaymentSelect("Select1", null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }


		[HttpGet]
		public ActionResult MakePayment(int id = 0)
		{
			if (id == 0)
			{
				return View(new tbl_MakePayment());
			}
			else
			{

				var tb = db.tbl_MakePaymentSelect("Details", id, null, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new tbl_MakePayment();
				vm.PrincipleAmount = tb.PrincipleAmount;
				vm.PaidFrom = tb.PaidFrom;
				vm.InterestAmount = tb.InterestAmount;
				vm.AccountName = tb.AccountName;
				vm.TotalAmount = tb.TotalAmount;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
		}


		[HttpPost]
		public ActionResult MakePayment( tbl_MakePayment conn, int id=0)
		{
			if (id == 0)
			{

				db.tbl_MakePaymentSelect("Insert", null, conn.PrincipleAmount, conn.InterestAmount, conn.Date, conn.TotalAmount, conn.PaidFrom, conn.AccountName, null);
				db.SubmitChanges();
				ModelState.Clear();
				return RedirectToAction("Makepayment");
			}
			else
			{
				db.tbl_MakePaymentSelect("Update", id, conn.PrincipleAmount, conn.InterestAmount, conn.Date, conn.TotalAmount, conn.PaidFrom, conn.AccountName, null);
				db.SubmitChanges();
				ModelState.Clear();
				return RedirectToAction("Makepayment");
				//return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_MakePaymentSelect("Delete", id, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

	}
}