using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DeliveryChallanController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: DeliveryChallan
        public ActionResult Index()
        {
            return View();
        }

        //'The DELETE statement conflicted with the REFERENCE constraint "FK__tbl_Deliv__Chall__68D28DBC". The conflict occurred in database "idealtec_inventory", table "dbo.tbl_DeliveryChallanInner", column 'ChallanNo'. The statement has been terminated.'

        [HttpGet]
        public ActionResult ShowChallanData()
        {
            var getdata = db.tbl_DeliveryChallanSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_DeliveryChallanSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_DeliveryChallanSelectResult challan)
        {
            //try
            //{
            //    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
            //    db.tbl_DeliveryChallanSelect("Insert", null, challan.PartyName, challan.BillingName, challan.BillingAddress, challan.PartyAddress,Convert.ToDateTime(challan.InvoiceDate), challan.DueDate, challan.StateofSupply, challan.ContactNo, challan.PaymentType, challan.TransportName, challan.DeliveryLocation, challan.VehicleNumber, challan.Deliverydate, challan.Description, challan.TransportCharges, challan.Image, challan.Tax1, challan.TaxAmount1, challan.CGST, challan.SGST, Convert.ToString(challan.TotalDiscount), challan.DiscountAmount1, challan.RoundFigure, challan.Total, challan.Received, challan.RemainingBal, challan.PaymentTerms,null,null,null,null,null,null, challan.Status, null, challan.ItemCategory, challan.Barcode, challan.IGST, challan.Company_ID, challan.CalTotal, challan.TaxShow, null);
            //    db.SubmitChanges();
            //    return RedirectToAction("Index");
            return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
            //    return View("Error", new HandleErrorInfo(e, "DeliveryChallan", "AdOrEdit"));
            //}
        }
    }
}