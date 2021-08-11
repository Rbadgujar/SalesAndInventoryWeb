using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Data.SqlClient;
using System.Configuration;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PurchaseBillController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: PurchaseBill
		public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index( string date ,string date2)
        {
              return View();
        }
		[HttpGet]
        public ActionResult Data(string date,string date2,string par)
        {
            if (par == "0")
            {
                var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null,Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
           
                var tb1 = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            

        }

        [HttpGet]
        public ActionResult show(string date)
        {
            var tb1 = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Detail(int id)
		{
			var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.BillNo == id);
			return View(tb);
		}
     
        
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where DeleteData='1'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["ItemName"].ToString(),
                                Value = sdr["ItemName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1'");
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
                                Text = sdr["PartyName"].ToString(),
                                Value = sdr["PartyName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
        }
        [HttpPost]
        public ActionResult AddPurchase(PartyDetailsForPurchase objpurchase)
        {
            var gstcount = objpurchase.TaxAmount1;
            var gst = gstcount / 2;

            tbl_PurchaseBill sale = new tbl_PurchaseBill()
            {
                PartyName = objpurchase.PartyName,
                BillingName = objpurchase.BillingName,
                ContactNo = objpurchase.ContactNo,
                RemainingBal = objpurchase.RemainingBal,
                Total = objpurchase.Total,
                PONo = objpurchase.PONo,
                PoDate = objpurchase.PoDate,
                Feild4 = objpurchase.Feild4,
                TaxAmount1 = objpurchase.TaxAmount1,
                TransportName = objpurchase.TransportName,
                DeliveryLocation = objpurchase.DeliveryLocation,
                Deliverydate = objpurchase.DeliveryDate,
                StateofSupply = objpurchase.StateOfSupply,
                BillDate = objpurchase.BillDate,
                DueDate = objpurchase.DueDate,
                SGST = gst,
                CGST = gst,
                Barcode = objpurchase.Barcode,
                Status = objpurchase.Status,
                VehicleNumber = objpurchase.VehicleNumber
               
            };
            db.tbl_PurchaseBills.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpurchase.listpurchasedetail)
            {
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_PurchaseBillInner inner = new tbl_PurchaseBillInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    BillNo = sale.BillNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount,
                    SaleTaxAmount = item.SaleTaxAmount,
                    CGST = finalgsr,
                    SGST=finalgsr,              
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_PurchaseBillInners.InsertOnSubmit(inner);
                db.SubmitChanges();
                TempData["msg"] = "Insert Data Sucessfully...";
            }        
            return Json(data:  new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet } );
        }

        public JsonResult GetFruitName(string id)
        {
            return Json(GetFruitNameById(id), JsonRequestBehavior.AllowGet);
        }
        private static List<tbl_PartyMaster> GetFruitNameById(string id)
        {
            string sql;
            List<tbl_PartyMaster> items2 = new List<tbl_PartyMaster>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName = @Id and DeleteData='1'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items2.Add(new tbl_PartyMaster()
                            {
                                BillingAddress = sdr["BillingAddress"].ToString(),
                                ContactNo = sdr["ContactNo"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items2;
        }
        public JsonResult GetFruitName1(string id)
        {
            return Json(GetFruitNameById1(id), JsonRequestBehavior.AllowGet);
        }
        private static List<tbl_ItemMaster> GetFruitNameById1(string id)
        {
            string sql;
            List<tbl_ItemMaster> items3 = new List<tbl_ItemMaster>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemName = @Id");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items3.Add(new tbl_ItemMaster()
                            {
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }


        [HttpGet]
        public ActionResult AddPurchase(int id = 0)
        {
          
                tbl_PurchaseBill bt = new tbl_PurchaseBill();
                bt.ListOfAccounts = ListOfItems();
                bt.ListOfParties = ListOfParties();
                return View(bt);
           
        }

        public ActionResult vits()
        {

            var tb = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
       [HttpPost]
        public ActionResult datewise(string m)
        {

            var tb = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult order()
        {

            var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddPurchaseUpdate(int id = 0)
        {
            var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.BillNo == id);
            var vm = new tbl_PurchaseBill();
            vm.BillNo = tb.BillNo;
            vm.PartyName = tb.PartyName;
            vm.BillingName = tb.BillingName;
            vm.ContactNo = tb.ContactNo;
            vm.BillDate = Convert.ToDateTime(tb.BillDate);
            vm.PoDate = Convert.ToDateTime(tb.PoDate);
            vm.DueDate = Convert.ToDateTime(tb.DueDate);
            vm.StateofSupply = tb.StateofSupply;
            vm.PaymentType = tb.PaymentType;
            vm.VehicleNumber = tb.VehicleNumber;
            vm.DeliveryLocation = tb.DeliveryLocation;
            vm.TransportName = tb.TransportName;
            vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
            vm.Description = tb.Description;
            vm.TransportCharges = tb.TransportCharges;
            vm.Tax1 = tb.Tax1;
            vm.TaxAmount1 = tb.TaxAmount1;
            vm.CGST = tb.CGST;
            vm.SGST = tb.SGST;
            vm.Paid = tb.Paid;
            vm.DiscountAmount1 = tb.DiscountAmount1;
            vm.TotalDiscount = tb.TotalDiscount;
            vm.RoundFigure = tb.RoundFigure;
            vm.Total = tb.Total;
            vm.PaymentTerms = tb.PaymentTerms;
            vm.RemainingBal = tb.RemainingBal;
            vm.Status = tb.Status;
            vm.Barcode = tb.Barcode;
            vm.IGST = tb.IGST;
            vm.Feild4 = tb.Feild4;
            vm.Feild1 = tb.Feild1;
            return Json(GetFruitNameById5(id), JsonRequestBehavior.AllowGet);
            return View(vm);
        }
        public JsonResult GetFruitName2(string id)
        {
            return Json(GetFruitNameById2(id), JsonRequestBehavior.AllowGet);
        }

        private static List<tbl_ItemMaster> GetFruitNameById2(string id)
        {
            string sql;
            List<tbl_ItemMaster> items3 = new List<tbl_ItemMaster>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE Barcode = @Id");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items3.Add(new tbl_ItemMaster()
                            {
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }
        public static List<tbl_PurchaseBillInner> GetFruitNameById5(int id)
        {
            string sql;
            List<tbl_PurchaseBillInner> items3 = new List<tbl_PurchaseBillInner>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PurchaseBillInner WHERE ID = @Id");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items3.Add(new tbl_PurchaseBillInner()
                            {
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }




        //[HttpPost]
        //public ActionResult AddPurchase(tbl_PurchaseBill emp,int id= 0)
        //{
        //	if (id == 0)
        //	{

        //		var tb = db.tbl_PurchaseBillselect("Insert", null, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
        //		db.SubmitChanges();

        //		return RedirectToAction("Index");
        //	}
        //	else
        //	{
        //		var tb = db.tbl_PurchaseBillselect("Update", id, emp.PONo, emp.PartyName, emp.BillingName, emp.ContactNo, emp.BillDate, emp.PoDate, emp.DueDate, emp.StateofSupply, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.CGST, emp.SGST, emp.TaxAmount1, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Paid, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.ItemCategory, emp.IGST, null, null, null, null, null);
        //		db.SubmitChanges();
        //		return RedirectToAction("Index");
        //	}

        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_PurchaseBillselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
    }
}