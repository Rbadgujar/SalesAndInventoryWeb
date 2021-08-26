
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

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
            var getdata = db.tbl_ItemMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString()), null, null, null,null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Data1()
        {
            var getdata = db.tbl_ItemMasterSelect("Maxcount", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult unit()
        {
            return View();
        }
        public ActionResult dublication(string id)
        {
            var getdata = db.tbl_ItemMasterSelect("dublication", null,id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
            return Json(new { data = getdata.Count }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult brdublication(string id)
        {
            var getdata1 = db.tbl_ItemMasterSelect("dublication1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, id, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
            return Json(new { data = getdata1.Count }, JsonRequestBehavior.AllowGet);

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
                tbl_ItemMaster mm = new tbl_ItemMaster();
                mm.ListOfParties = ListOfParties();
                return View(mm);
            }
            else
            {
                var tb = db.tbl_ItemMasterSelect("Detail", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString()), null, null, null,null).Single(x => x.ItemID == id);
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
                vm.OpeningQty = tb.OpeningQty;
                vm.MinimumStock = tb.MinimumStock;
                vm.Expdate = tb.Expdate;
                vm.HSNCode = tb.HSNCode;
                vm.OpeningQty = tb.OpeningQty;
                vm.PurchasePrice = tb.PurchasePrice;
              //  vm.SalePrice = tb.SalePrice;
                vm.atPrice = tb.atPrice;
                return View(vm);
            }
        }
        public string pataa="";
        [HttpPost]
        public ActionResult Additem(HttpPostedFileBase file ,tbl_ItemMaster item, int id = 0)
        {
            if(item.MinimumStock==null)
            {
                item.MinimumStock = 0;

            }if(item.OpeningQty==null)
            {
                item.OpeningQty = 0;
            }
            if (item.Discount == null)
            {
                item.Discount = 0;
            }

            if (id == 0)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/Iteimage"), fileName);
                        file.SaveAs(path);
                        pataa = "~/images/Iteimage" + fileName;
                    }
                }
                catch(Exception ew)
                {
                   
                }
                // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
                db.tbl_ItemMasterSelect("Insert1", null, item.ItemName, item.HSNCode, item.BasicUnit,pataa, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode,Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit,item.Discount);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {

                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/Iteimage"), fileName);
                        file.SaveAs(path);
                        pataa = "~/images/Iteimage" + fileName;
                    }
                }
                catch (Exception ew)
                {

                }
                db.tbl_ItemMasterSelect("Update", null, item.ItemName, item.HSNCode, item.BasicUnit,pataa, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode,Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit, item.Discount);
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ItemTraking()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ItemTraking( string id)
        {
            db.tbl_UnitMasterUnit("Insert", null,id,null, null,null, Convert.ToInt32(Session["UserId"].ToString()));
            db.SubmitChanges();
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

        }







        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            items1.Add(new SelectListItem { Text = "Bags(Bag)", Value = "Bags" });
            items1.Add(new SelectListItem { Text = "Bottles(Btl)", Value = "Bottles" });
            items1.Add(new SelectListItem { Text = "Box(Box))", Value = "Box" });
            items1.Add(new SelectListItem { Text = " Bandal(Bdl)", Value = " Bandal" });
            items1.Add(new SelectListItem { Value = "Cans", Text = "Can(can)" });
            items1.Add(new SelectListItem { Value = "Cartons" ,Text="Cartons(ctn)"  });
            items1.Add(new SelectListItem { Text = "Tablets(Tbs)", Value = "Tablets" });
            items1.Add(new SelectListItem { Text = "Square Meters(smt)", Value = "Square Meter" });
            items1.Add(new SelectListItem { Text = "SquareFeet(Sqf)", Value = "Square Feet" });
            items1.Add(new SelectListItem { Text = "Rolls(rol)", Value = "Rolls" });
            items1.Add(new SelectListItem { Text = "Dozens(Dzn)", Value = "Dozens" });
            items1.Add(new SelectListItem { Text = "Grammes(gm)", Value = "Grammes" });
            items1.Add(new SelectListItem { Text = "Kilogarms(kg)", Value = "Kilogarms" });
            items1.Add(new SelectListItem { Text = "Litre(ltr)", Value = "Litre" });
            items1.Add(new SelectListItem { Text = "Meters(mtr)", Value = "Meters" });
            items1.Add(new SelectListItem { Text = "Mililiter(mL)", Value = "Mililiter" });
            items1.Add(new SelectListItem { Text = "Packs(pca)", Value = "Packs" });




            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //sql = string.Format("SELECT * FROM tbl_UnitMaster where  Company_ID=" + MainLoginController.companyid1 + " and  DeleteData='1' ");
                sql = string.Format("SELECT * FROM tbl_UnitMaster where DeleteData='1' ");

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items1.Add(new SelectListItem
                            {
                                Text = sdr["UnitName"].ToString(),
                                Value = sdr["UnitName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            var tb = db.tbl_ItemMasterSelect("Detail", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).Single(x => x.ItemID == id);
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
            vm.OpeningQty = tb.OpeningQty;
            vm.MinimumStock = tb.MinimumStock;
            vm.Expdate = tb.Expdate;
            vm.HSNCode = tb.HSNCode;
            vm.OpeningQty = tb.OpeningQty;
            vm.PurchasePrice = tb.PurchasePrice;
            vm.SalePrice = tb.SalePrice;
            vm.atPrice = tb.atPrice;
            return View(vm);



        }


       

        [HttpPost]
        public ActionResult AddOrEdit(HttpPostedFileBase file,tbl_ItemMaster item,int id=0)
        {

            if (item.MinimumStock == null)
            {
                item.MinimumStock = 0;

            }
            if (item.OpeningQty == null)
            {
                item.OpeningQty = 0;
            }
            if (item.Discount == null)
            {
                item.Discount = 0;
            }
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/Iteimage"), fileName);
                    file.SaveAs(path);
                    pataa = "~/images/Iteimage" + fileName;
                }
            }
            catch (Exception ew)
            {

            }
            db.tbl_ItemMasterSelect("Update1",id, item.ItemName, item.HSNCode, item.BasicUnit, pataa, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit, item.Discount);
            db.SubmitChanges();
            return RedirectToAction("Index");


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
                var getdata = db.tbl_ItemMasterSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString()), null, null, null,null).ToList();
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
