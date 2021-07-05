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
    public class ItemMasterController : Controller
    {
        // idealtec_inventoryEntities10 ew = new idealtec_inventoryEntities10();
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: ItemMaster
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Data(int id, tbl_ItemMasterSelectResult item)
        {

            var getdata = db.tbl_ItemMasterSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult itemdata()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_ItemMaster> estimate = db.tbl_ItemMaster.ToList<tbl_ItemMaster>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, tbl_ItemMasterSelectResult item)
        {
            try
            {
                var getdata = db.tbl_ItemMasterSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(int id, tbl_ItemMasterSelectResult item)
        {
            try
            {
              //                      ItemName,HSNCode ,BasicUnit,SecondaryUnit,ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,Date,ItemLocation,TrackingMRP,BatchNo,SerialNo,MFgdate,Expdate,Size ,Description                                                           ,MinimumStock ,Image1,Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
                //db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.TaxForPurchase, item.PurchaseTaxAmount, item.OpeningQty, item.atPrice, item.Date, item.TrackingMRP,item.BatchNo, item.SerialNo,item.MFgdate,item.Expdate,item.Size,item.Description,item.MinimumStock,item.Image1,null,null,null,null,null,item.Barcode,null,item.Cess,item.saleTax,item.PurchaseTax,item.Profit);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //[HttpGet]
        //public ActionResult AddOrEdit(int id = 0)
        //{
        //    if (id == 0)
        //        return View(new tbl_ItemMaster());
        //    else
        //    {
        //        using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //        {
        //            return View(db.tbl_ItemMaster.Where(x => x.ItemID == id).FirstOrDefault<tbl_ItemMaster>());
        //        }
        //    }
        //}

        //[HttpPost]
        //public ActionResult AddOrEdit(tbl_ItemMaster emp)
        //{
        //    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //    {
        //        if (emp.ItemID == 0)
        //        {
        //            db.tbl_ItemMaster.Add(emp);
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            db.Entry(emp).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }


        //}


        // GET: ItemMaster/Details/5


        //[HttpPost]
        //public ActionResult AddOrEdit(tbl_ItemMasterSelectResult item)
        //{
        //    try
        //    {

        //        //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
        //        db.tbl_ItemMasterSelect("Insert",null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.TaxForPurchase, item.PurchasePrice, item.PurchaseTaxAmount, item.OpeningQty, item.atPrice, item.Date,item.ItemLocation,item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size,item.Description,item.MinimumStock, item.Image1,null,null,null,null,null,null, item.Barcode, null,item.Cess,item.saleTax,item.PurchaseTax, item.Profit);
        //        db.SubmitChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
