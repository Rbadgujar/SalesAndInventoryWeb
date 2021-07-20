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

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_QuotationSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.RefNo == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new tblQuotation());
            }
            else
            {
                //var tb = db.tbl_QuotationSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.RefNo == id);
                //var vm = new tblQuotation();
                //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
                //UnusedAmount,Total,Status,image
                vm.PartyName = tb.PartyName;
                vm.BillingAddress = tb.BillingAddress;
                vm.Date = Convert.ToDateTime(tb.Date);
                vm.StateofSupply = tb.StateofSupply;
                vm.ContactNo = tb.ContactNo;
                vm.Description = tb.Description;
                //vm.Image = tb.Image;
                vm.Tax1 = tb.Tax1;
                vm.TaxAmount1 = tb.TaxAmount1;
                vm.CGST = tb.CGST;
                vm.SGST = tb.SGST;
                vm.TotalDiscount = tb.TotalDiscount;
                vm.DiscountAmount1 = tb.DiscountAmount1;
                vm.RoundFigure = tb.RoundFigure;
                vm.Status = tb.Status;
                vm.Total = tb.Total;
                vm.Barcode = tb.Barcode;
                vm.Itemcatgory = tb.ItemCategory;
                vm.CalTotal = tb.CalTotal;
                vm.TaxShow = tb.TaxShow;
                vm.Discount = tb.Discount;
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_QuotationSelectResult esti, tbl_QuotationInnerspResult inner,int id=0)
        {
            if (id == 0)
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    //db.tbl_QuotationSelect("Insert", null, esti.PartyName, esti.BillingAddress, Convert.ToDateTime(esti.Date), esti.StateofSupply, esti.ContactNo, esti.Description, esti.Image, esti.Tax1, esti.TaxAmount1, esti.CGST, esti.SGST, esti.TotalDiscount, esti.DiscountAmount1, esti.RoundFigure, esti.Total, null, null, null, null, null, esti.Status, null, null, esti.Barcode, esti.Company_ID, esti.ItemCategory, esti.CalTotal, esti.TaxShow, esti.Discount);
                    //db.tbl_QuotationInnersp("Insert", null, inner.ItemID, inner.CategoryType, inner.ItemName, inner.BasicUnit, inner.ItemCode, inner.SalePrice, inner.TaxForSale, inner.SaleTaxAmount, inner.Qty, inner.freeQty, inner.ItemAmount, inner.Discount, inner.DiscountAmount, inner.Company_ID, inner.RefNo, inner.CGST, inner.SGST, inner.IGST, inner.CalTotal);
                    //db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, "EstimateQuatation", "AdOrEdit"));
                }
            }
            else
            {
                try
                {
                    //db.tbl_QuotationSelect("Update", null, esti.PartyName, esti.BillingAddress, Convert.ToDateTime(esti.Date), esti.StateofSupply, esti.ContactNo, esti.Description, esti.Image, esti.Tax1, esti.TaxAmount1, esti.CGST, esti.SGST, esti.TotalDiscount, esti.DiscountAmount1, esti.RoundFigure, esti.Total, null, null, null, null, null, esti.Status, null, null, esti.Barcode, esti.Company_ID, esti.ItemCategory, esti.CalTotal, esti.TaxShow, esti.Discount);
                    //db.tbl_QuotationInnersp("Update", null, inner.ItemID, inner.CategoryType, inner.ItemName, inner.BasicUnit, inner.ItemCode, inner.SalePrice, inner.TaxForSale, inner.SaleTaxAmount, inner.Qty, inner.freeQty, inner.ItemAmount, inner.Discount, inner.DiscountAmount, inner.Company_ID, inner.RefNo, inner.CGST, inner.SGST, inner.IGST, inner.CalTotal);
                    //db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
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