using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleOrderController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleOrder
        public ActionResult SaleOrder()
        {
            return View();
        }
        public ActionResult daaa()
        {
            return View();
        }

        [HttpGet]
        public ActionResult showSaleOrder()
        {
            var getdata = db.tbl_SaleOrderSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_SaleOrderSelectResult order)
        {
            try
            {
                 object id1;

                //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                id1 =   db.tbl_SaleOrderSelect("Insert", null, order.PartyName,order.BillingName, order.ContactNo, Convert.ToDateTime(order.OrderDate), order.DueDate, order.StateofSupply, order.PaymentType, order.TransportName, order.DeliveryLocation, order.VehicleNumber, order.Deliverydate, order.Description, order.TransportCharges, order.Image, order.Tax1, order.CGST, order.SGST, order.TaxAmount1, Convert.ToString(order.TotalDiscount), order.DiscountAmount1, order.RoundFigure, order.Total, order.Received, order.RemainingBal, order.PaymentTerms, null, null, null, null, null,  order.Status, null, null, order.ItemCategory, order.Barcode, order.IGST, order.Company_ID,  order.TaxShow, null,order.CalTotal);
                db.SubmitChanges();
             //   db.tbl("Insert",id1, order.PartyName, order.BillingName, order.ContactNo, Convert.ToDateTime(order.OrderDate), order.DueDate, order.StateofSupply, order.PaymentType, order.TransportName, order.DeliveryLocation, order.VehicleNumber, order.Deliverydate, order.Description, order.TransportCharges, order.Image, order.Tax1, order.CGST, order.SGST, order.TaxAmount1, Convert.ToString(order.TotalDiscount), order.DiscountAmount1, order.RoundFigure, order.Total, order.Received, order.RemainingBal, order.PaymentTerms, null, null, null, null, null, order.Status, null, null, order.ItemCategory, order.Barcode, order.IGST, order.Company_ID, order.TaxShow, null, order.CalTotal);
                db.SubmitChanges();
                return RedirectToAction("SaleOrder");
                //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "SaleOrder", "AddOrEdit"));
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_SaleOrderSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}
