
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
        public ActionResult Data()
        {
            var getdata = db.tbl_ItemMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,MainLoginController.companyid1, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult unit()
        {
            return View();
        }


       public ActionResult Addparty()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Additem(int id=0)
        {
            if (id == 0)
            {
                return View(new tbl_ItemMaster());
            }
            else
            {
                var tb = db.tbl_ItemMasterSelect("Detail1", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ItemID == id);
                var vm = new tbl_ItemMaster();
                vm.ItemName = tb.ItemName;
                vm.BasicUnit = tb.BasicUnit;
                vm.ItemCode = tb.ItemCode;
                vm.ItemCategory = tb.ItemCategory;
                vm.TaxForSale = tb.TaxForSale;
                vm.TrackingMRP = tb.TrackingMRP;
                vm.Size = tb.Size;
                vm.BatchNo = tb.BatchNo;
                vm.SerialNo = tb.SerialNo;
                vm.MFgdate = tb.MFgdate;
                vm.Expdate = tb.Expdate;
                vm.HSNCode = tb.HSNCode;
                vm.OpeningQty = tb.OpeningQty;
                vm.PurchasePrice = tb.PurchasePrice;
              //  vm.SalePrice = tb.SalePrice;
                vm.atPrice = tb.atPrice;
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Additem(tbl_ItemMaster item, int id = 0)
        {
            if (id == 0)
            {
                // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
                db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, null, null, null, item.Profit);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {

                db.tbl_ItemMasterSelect("Update", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, null, null, null, item.Profit);
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ItemTraking()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {

            if (id == 0)
            {
                return View(new tbl_ItemMaster());
            }
            else
            {
                var tb = db.tbl_ItemMasterSelect("Detail1", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ItemID == id);
                var vm = new tbl_ItemMaster();
                vm.ItemName = tb.ItemName;
                vm.HSNCode = tb.HSNCode;
                vm.OpeningQty = tb.OpeningQty;
                vm.PurchasePrice = tb.PurchasePrice;
                //m.SalePrice = tb.SalePrice;
                vm.atPrice = tb.atPrice;
                return View(vm);
            }
         
        }

      
        [HttpPost]
        public ActionResult AddOrEdit(tbl_ItemMaster item,int id=0)
        {
           
                if (id == 0)
                {
                    // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
                    db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, null, null, null, item.Profit);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                    db.tbl_ItemMasterSelect("Update", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, null, null, null, item.Profit);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    
                }
            
        }
        //public ActionResult itemdata()
        //{
        //    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //    {
        //        List<tbl_ItemMaster> estimate = db.tbl_ItemMaster.ToList<tbl_ItemMaster>();
        //        return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_ItemMasterSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }
        
      

    }
}
