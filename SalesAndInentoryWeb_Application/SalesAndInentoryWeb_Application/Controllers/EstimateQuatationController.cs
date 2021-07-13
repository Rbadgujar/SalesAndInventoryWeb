using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class EstimateQuatationController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: EstimateQuatation
        public ActionResult Index()
        {
			return View();
        }

        [HttpGet]
        public ActionResult EstimateData()
        {
            var getdata = db.tbl_QuotationSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_QuotationSelectResult esti, tbl_QuotationInnerspResult inner)
        {
            try
            {
                //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                db.tbl_QuotationSelect("Insert", null, esti.PartyName, esti.BillingAddress, Convert.ToDateTime(esti.Date), esti.StateofSupply, esti.ContactNo, esti.Description, esti.Image, esti.Tax1, esti.TaxAmount1,esti.CGST,esti.SGST, esti.TotalDiscount, esti.DiscountAmount1, esti.RoundFigure, esti.Total, null, null, null, null, null, esti.Status, null, null, esti.Barcode, esti.Company_ID, esti.ItemCategory, esti.CalTotal, esti.TaxShow, esti.Discount);
                db.tbl_QuotationInnersp("Insert", null, inner.ItemID, inner.CategoryType, inner.ItemName, inner.BasicUnit, inner.ItemCode, inner.SalePrice, inner.TaxForSale, inner.SaleTaxAmount, inner.Qty, inner.freeQty, inner.ItemAmount, inner.Discount, inner.DiscountAmount, inner.Company_ID, inner.RefNo, inner.CGST, inner.SGST, inner.IGST, inner.CalTotal );
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "EstimateQuatation", "AdOrEdit"));
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_QuotationSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}