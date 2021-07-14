using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CreditnoteController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Creditnote
        public ActionResult CreditNote()
        {
            return View();
        }

        //public ActionResult show()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult creditnotedata()
        {
           var getdata = db.tbl_CreditNote1Select("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCredit(int id)
        {
            var getdata = db.tbl_CreditNote1Select("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_CreditNote1SelectResult credit)
        {
            //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
            db.tbl_CreditNote1Select("Insert", credit.InvoiceNo, null, credit.PartyName, credit.BillingName, credit.PONumber, credit.PODate, Convert.ToDateTime(credit.InvoiceDate), credit.DueDate, credit.StateofSupply, credit.ContactNo, credit.PaymentType, credit.TransportName, credit.DeliveryLocation, credit.VehicleNumber, credit.Deliverydate, credit.Description, credit.TransportCharges, credit.Image, credit.Tax1, credit.TaxAmount1, credit.CGST, credit.SGST, credit.TotalDiscount, credit.DiscountAmount1, credit.RoundFigure, credit.Total, credit.Received, credit.RemainingBal, credit.PaymentTerms, null, null, null, null, null, null, credit.Status, credit.ItemCategory, credit.Barcode, credit.IGST, credit.Company_ID, credit.CalTotal, credit.TaxShow, credit.Discount);
           // db.tbl_CreditNote1Select("Insert", credit.InvoiceNo, null, credit.PartyName, credit.BillingName, credit.PONumber, null, null, credit.DueDate, credit.StateofSupply, credit.ContactNo, credit.PaymentType, credit.TransportName, credit.DeliveryLocation, credit.VehicleNumber, credit.Deliverydate, credit.Description, credit.TransportCharges, credit.Image, credit.Tax1, credit.TaxAmount1, credit.CGST, credit.SGST, credit.TotalDiscount, credit.DiscountAmount1, credit.RoundFigure, credit.Total, credit.Received, credit.RemainingBal, credit.PaymentTerms, null, null, null, null, null, null, credit.Status, credit.ItemCategory, credit.Barcode, credit.IGST, credit.Company_ID, credit.CalTotal, credit.TaxShow, credit.Discount);
            db.SubmitChanges();
            return RedirectToAction("CreditNote");
            //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);

        }
    }
}
