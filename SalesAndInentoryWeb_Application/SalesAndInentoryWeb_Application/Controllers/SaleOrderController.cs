using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Data.SqlClient;
using System.Data;
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
        [HttpPost]
       
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
        public ActionResult AddOrEdit(int id=0)
        {
            //if (id == 0)
            //{
            //    tbl_SaleOrder objbank = new tbl_SaleOrder();
            //    objbank.ListOfAccounts = (from obj in db.tbl_ItemMasters
            //                              select new SelectListItem
            //                              {
            //                                  Text = obj.ItemName,
            //                                  Value = obj.ItemID.ToString()
            //                              });
            //    return View(objbank);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(PartyDetails objpartydetails)
        {
            tbl_SaleOrder sale = new tbl_SaleOrder()
            {               
                PartyName = objpartydetails.PartyName,
                BillingName=objpartydetails.BillingName,
                ContactNo = objpartydetails.ContactNo,                             
                RemainingBal = objpartydetails.RemainingBal,
                CalTotal = objpartydetails.CalTotal,
                TransportName = objpartydetails.TransportName,
                DeliveryLocation = objpartydetails.DeliveryLocation,
                Deliverydate = objpartydetails.DeliveryDate,
                StateofSupply = objpartydetails.StateOfSupply,
                OrderDate = objpartydetails.OrderDate,
                DueDate = objpartydetails.DueDate,
                Barcode = objpartydetails.Barcode,
                Status = objpartydetails.Status,
                VehicleNumber = objpartydetails.VehicleNumber

            };
            db.tbl_SaleOrders.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpartydetails.listofitemdetail)
            {
                tbl_SaleOrderInner inner = new tbl_SaleOrderInner()
                {
                    ItemName=item.ItemName,
                    SalePrice=item.SalePrice,
                    OrderNo=sale.OrderNo,
                    ItemID=item.ItemID,
                    TaxForSale=item.TaxForSale,
                    Discount=item.Discount,
                    DiscountAmount=item.DiscountAmount,
                    SaleTaxAmount=item.SaleTaxAmount,
                    ItemTotal=item.ItemTotal,
                    Qty=item.Qty
                };
                db.tbl_SaleOrderInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
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
