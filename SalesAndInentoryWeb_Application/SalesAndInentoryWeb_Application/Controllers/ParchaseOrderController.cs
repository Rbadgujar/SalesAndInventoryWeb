using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ParchaseOrderController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: ParchaseOrder
		public ActionResult PurchaseOrder()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Purchaseorderdata(string date, string date2, string par)
        {
            if (par == "0")
            {
                var tb = db.tbl_PurchaseOrderSelect("datetotdate", null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_PurchaseOrderSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.OrderNo == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrder(int id=0)
		{
			if (id == 0)
			{
				return View(new tbl_PurchaseOrder());
			}
			else
			{
				var tb = db.tbl_PurchaseOrderSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.OrderNo == id);
				var vm = new tbl_PurchaseOrder();
				
				vm.PartyName = tb.PartyName;
				vm.BillingName = tb.BillingName;
				vm.ContactNo = tb.ContactNo;
				vm.DueDate = Convert.ToDateTime(tb.DueDate);
				vm.StateofSupply = tb.StateofSupply;
				vm.PaymentType = tb.PaymentType;
				vm.VehicleNumber = tb.VehicleNumber;
				vm.OrderDate = tb.OrderDate.ToString();
				//vm.DeliveryLocation = tb.DeliveryLocation;
				vm.TransportName = tb.TransportName;
				vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
				//vm.Description = tb.Description;
				//vm.TransportCharges = tb.TransportCharges;
				//vm.Tax1 = tb.Tax1;
				//vm.TaxAmount1 = tb.TaxAmount1;
				//vm.CGST = tb.CGST;
				//vm.SGST = tb.SGST;
				vm.Paid = tb.Paid;
				//vm.DiscountAmount1 = tb.DiscountAmount1;
				//vm.TotalDiscount = tb.TotalDiscount;
				//vm.RoundFigure = tb.RoundFigure;
				vm.Total = tb.Total;
				//vm.PaymentTerms = tb.PaymentTerms;
				vm.RemainingBal = tb.RemainingBal;
				vm.Status = tb.Status;
				vm.Barcode = tb.Barcode;
				//vm.IGST = tb.IGST;
				vm.Feild4 = tb.Feild4;
				vm.Feild1 = tb.Feild1;
				return View(vm);
			}
		
		}

		[HttpPost]
		public ActionResult AddOrder(tbl_PurchaseOrder emp,int id=0)
		{
			if (id == 0)
			{

				var tb = db.tbl_PurchaseOrderSelect("Insert", null, null, emp.PartyName, emp.BillingName, emp.ContactNo, Convert.ToDateTime(emp.OrderDate), emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, emp.TotalDiscount, emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, emp.Barcode, null,Convert.ToString(emp.IGST), null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("PurchaseOrder");
			}
			else
			{
				var tb = db.tbl_PurchaseOrderSelect("Update", null, id, emp.PartyName, emp.BillingName, emp.ContactNo, Convert.ToDateTime(emp.OrderDate), emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1,emp.TotalDiscount, emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, emp.Barcode, null, Convert.ToString(emp.IGST), null, null, null, null);
				db.SubmitChanges();

				return RedirectToAction("PurchaseOrder");
			}

		}



		[HttpPost]
		public ActionResult Dele(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
