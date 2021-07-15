using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ExpenceController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: Expence
		public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Expencescategory()
        {
            return View();
        }

	
        public ActionResult ExpenceData()
        { 
			var tb = db.tbl_ExpensesSelect("Select1", null, null, null, null, null, null,null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);

		}

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_ExpensesSelectResult exp)
        {
            //try
            //{
            //    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
            //    db.tbl_ExpensesSelect("Insert", null, exp.ExpenseCategory, exp.Date, exp.Description, exp.Image, exp.Total, exp.Paid, exp.Balance, exp.AdditinalFeild1, exp.AdditionalFeild2, exp.Status, exp.TableName, exp.compid);
            //    db.SubmitChanges();
            //    return RedirectToAction("Index");
         return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
            //    return View("Error", new HandleErrorInfo(e, "Expence", "AddOrEdit"));
            //}
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tb = db.tbl_ExpensesSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}