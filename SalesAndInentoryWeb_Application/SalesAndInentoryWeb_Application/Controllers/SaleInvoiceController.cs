using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;


namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleInvoiceController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleInvoice
        //public ActionResult Index()
        //{
           
        //    return View();
        //}
        public ActionResult SaleIndexpage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowInvoiceData()
        {
            var getinvoice = db.tbl_SaleInvoiceSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getinvoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaleInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaleInvoice(tbl_SaleInvoiceSelectResult invoice)
        {
            try
            {
                //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                db.tbl_SaleInvoiceSelect("Insert", null, invoice.PartyName, invoice.BillingName, invoice.ContactNo, invoice.PONumber, invoice.PoDate, Convert.ToDateTime(invoice.InvoiceDate),  invoice.StateofSupply, invoice.PaymentType, invoice.TransportName, invoice.DeliveryLocation, invoice.VehicleNumber, invoice.Deliverydate, invoice.Description, invoice.TransportCharges, invoice.Image, invoice.Tax1, invoice.CGST, invoice.SGST, invoice.TaxAmount1, invoice.TotalDiscount, invoice.DiscountAmount1, invoice.RoundFigure, invoice.Total, invoice.Received, invoice.RemainingBal, invoice.DueDate, invoice.PaymentTerms, null, null, null, null, null, invoice.Status, null, null, invoice.ItemCategory, invoice.Barcode, invoice.IGST, invoice.Company_ID, invoice.Discount,invoice.TaxAmountShow, invoice.Caltotal, invoice.totalcgst, invoice.totalsgst, invoice.totaligst, invoice.EWayBillNo);
                db.SubmitChanges();
                return RedirectToAction("SaleIndexpage");
                //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
           var getdata = db.tbl_SaleInvoiceSelect("Delete", id,null,null,null,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           db.SubmitChanges();
           return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartPartial()
        {
            var model = new object[0];
            return PartialView("~/Views/DashBord/_ChartPartial.cshtml", model);
        }

        SalesAndInentoryWeb_Application.CompanyDataClassDataContext db1 = new SalesAndInentoryWeb_Application.CompanyDataClassDataContext();

        public ActionResult ChartPartial1()
        {
            var model = db1.tbl_SaleInvoices;
            return PartialView("~/Views/Home/_ChartPartial1.cshtml", model);
        }
    }
}