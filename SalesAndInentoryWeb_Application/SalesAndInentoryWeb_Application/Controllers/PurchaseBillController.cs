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
				vm.PONo = tb.PONo;
				vm.PartyName = tb.PartyName;
				vm.BillingName = tb.BillingName;
				vm.ContactNo = tb.ContactNo;
				vm.BillDate = Convert.ToDateTime(tb.BillDate);
				vm.PoDate = Convert.ToDateTime(tb.PoDate);
				vm.DueDate = Convert.ToDateTime(tb.DueDate);
				vm.StateofSupply = tb.StateofSupply;
				vm.PaymentType = tb.PaymentType;
				vm.VehicleNumber = tb.VehicleNumber;
				vm.DeliveryLocation = tb.DeliveryLocation;
				vm.TransportName = tb.TransportName;
				vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
				vm.Description = tb.Description;
				vm.TransportCharges = tb.TransportCharges;
				vm.Tax1 = tb.Tax1;
				vm.TaxAmount1 = tb.TaxAmount1;
				vm.CGST = tb.CGST;
				vm.SGST = tb.SGST;
				vm.Paid = tb.Paid;
				vm.DiscountAmount1 = tb.DiscountAmount1;
				vm.TotalDiscount = tb.TotalDiscount;
				vm.RoundFigure = tb.RoundFigure;
				vm.Total = tb.Total;
				vm.PaymentTerms = tb.PaymentTerms;
				vm.RemainingBal = tb.RemainingBal;
				vm.Status = tb.Status;
				vm.Barcode = tb.Barcode;
				vm.IGST = tb.IGST;
				vm.Feild4 = tb.Feild4;
				vm.Feild1 = tb.Feild1;
				return View(vm);
			}
		
		}

        public ActionResult vits()
        {

            var tb = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult order()
        {

            var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
           
		[HttpPost]
		public ActionResult AddPurchase(tbl_PurchaseBill emp,int id= 0)
		{
			if (id == 0)
			{

				var tb = db.tbl_PurchaseBillselect("Insert", null, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
				db.SubmitChanges();
				 
				return RedirectToAction("Index");
			}
			else
			{
				var tb = db.tbl_PurchaseBillselect("Update", id, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
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