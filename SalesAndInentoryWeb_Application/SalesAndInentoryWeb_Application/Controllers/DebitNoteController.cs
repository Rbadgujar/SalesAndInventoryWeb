
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DebitNoteController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: DebitNote
		public ActionResult DebitNote()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Debitdata(string date, string date2, string par)
        {
            if (par == "0")
            {
                var tb = db.tbl_DebitNoteSelect("datetodate", null, null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_DebitNoteSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_DebitNoteSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ReturnNo == id);
			return View(tb);
		}

		[HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
			if (id == 0)
			{
				   return View(new tbl_DebitNote());
			}
			else
			{
				var tb = db.tbl_DebitNoteSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ReturnNo == id);
				var vm = new tbl_DebitNote();
				vm.PartyName = tb.PartyName;
				vm.BillingName = tb.BillingName;
				vm.ContactNo = tb.ContactNo;
				vm.PONumber = tb.PONumber;
				vm.PODate = Convert.ToDateTime(tb.PODate);
				vm.Barcode = tb.Barcode;
				vm.Feild4 = tb.Feild4;
				vm.StateofSupply = tb.StateofSupply;
				vm.InvoiceDate = Convert.ToDateTime(tb.InvoiceDate);
				vm.PaymentType = tb.PaymentType;
				vm.TransportName = tb.TransportName;
				vm.VehicleNumber = tb.VehicleNumber;
				vm.Feild1 = tb.Feild1;
				vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
				vm.Status = tb.Status;
				vm.Total = tb.Total;
				vm.Received = tb.Received;
				vm.RemainingBal = tb.RemainingBal;
				return View(vm);
			}
        }

		[HttpPost]
		public ActionResult AddOrEdit(tbl_DebitNote emp,int id=0)
		{
			if (id == 0)
			{

				var tb = db.tbl_DebitNoteSelect("Insert", emp .InvoiceNo, null, emp.PartyName, emp.BillingName, emp.PONumber, emp.PODate, emp.InvoiceDate, emp.DueDate, emp.StateofSupply,emp.ContactNo, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1,emp.TaxAmount1, emp.CGST, emp.SGST, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Received, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.IGST,null, null, null, null, null);
				db.SubmitChanges();

				return RedirectToAction("DebitNote");
			}
			else
			{
				var tb = db.tbl_DebitNoteSelect("Update", emp.InvoiceNo, id, emp.PartyName, emp.BillingName, emp.PONumber, emp.PODate, emp.InvoiceDate, emp.DueDate, emp.StateofSupply, emp.ContactNo, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.TaxAmount1, emp.CGST, emp.SGST, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Received, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.IGST, null, null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("DebitNote");
			}
		}

		[HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_DebitNoteSelect("Delete", null, id,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ComboBoxPartial()
        {
            return PartialView("_ComboBoxPartial");
        }
    }
}
