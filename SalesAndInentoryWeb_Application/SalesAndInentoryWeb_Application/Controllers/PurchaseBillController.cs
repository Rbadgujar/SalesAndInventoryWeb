using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PurchaseBillController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: PurchaseBill
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
        public ActionResult Data()
        {
			var tb = db.tbl_PurchaseBillselect("Select1", null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.BillNo == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddPurchase(int id=0)
		{
			if (id == 0)
			{
				return View(new tbl_PurchaseBill());
			}
			else
			{
				var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.BillNo == id);
				var vm = new tbl_PurchaseBill();
				//vm.AccountName = tb.AccountName;
				//vm.BankName = tb.BankName;
				//vm.AccountNo = tb.AccountNo;
				//vm.OpeningBal = tb.OpeningBal;
				//vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
		
		}

		[HttpPost]
		public ActionResult AddPurchase(tbl_PurchaseBill emp,int id= 0)
		{
			if (id == 0)
			{

				var tb = db.tbl_PurchaseBillselect("Insert", null, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, null, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
			else
			{
				var tb = db.tbl_PurchaseBillselect("Update", id, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, null, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
			
		}

		[HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_PurchaseBillselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
    }
}