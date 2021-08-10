using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using SalesAndInentoryWeb_Application.ViewModel;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ParchaseOrderController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: ParchaseOrder
		public ActionResult PurchaseOrder()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Purchaseorderdata(string date, string date2, string par)
        {
            if (par == "0")
            {
                var tb = db.tbl_PurchaseOrderSelect("datetotdate", null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_PurchaseOrderSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.OrderNo == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrder(int id=0)
		{
            //if (id == 0)
            //{
            //	return View(new tbl_PurchaseOrder());
            //}
            //else
            //{
            //	var tb = db.tbl_PurchaseOrderSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.OrderNo == id);
            //	var vm = new tbl_PurchaseOrder();

            //	vm.PartyName = tb.PartyName;
            //	vm.BillingName = tb.BillingName;
            //	vm.ContactNo = tb.ContactNo;
            //	vm.DueDate = Convert.ToDateTime(tb.DueDate);
            //	vm.StateofSupply = tb.StateofSupply;
            //	vm.PaymentType = tb.PaymentType;
            //	vm.VehicleNumber = tb.VehicleNumber;
            //             vm.OrderDate = tb.OrderDate;
            //	//vm.DeliveryLocation = tb.DeliveryLocation;
            //	vm.TransportName = tb.TransportName;
            //	vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
            //	//vm.Description = tb.Description;
            //	//vm.TransportCharges = tb.TransportCharges;
            //	//vm.Tax1 = tb.Tax1;
            //	//vm.TaxAmount1 = tb.TaxAmount1;
            //	//vm.CGST = tb.CGST;
            //	//vm.SGST = tb.SGST;
            //	vm.Paid = tb.Paid;
            //	//vm.DiscountAmount1 = tb.DiscountAmount1;
            //	//vm.TotalDiscount = tb.TotalDiscount;
            //	//vm.RoundFigure = tb.RoundFigure;
            //	vm.Total = tb.Total;
            //	//vm.PaymentTerms = tb.PaymentTerms;
            //	vm.RemainingBal = tb.RemainingBal;
            //	vm.Status = tb.Status;
            //	vm.Barcode = tb.Barcode;
            //	//vm.IGST = tb.IGST;
            //	vm.Feild4 = tb.Feild4;
            //	vm.Feild1 = tb.Feild1;
            //	return View(vm);
            //}
            tbl_PurchaseOrder bt = new tbl_PurchaseOrder();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfParties = ListOfParties();
            return View(bt);
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName = @Id");
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

        [HttpPost]
		public ActionResult AddOrder(PartyDetailPurchaseOrder objpurchaseorder)
		{
            var gstcount = objpurchaseorder.TaxAmount1;
            var gst = gstcount / 2;

            tbl_PurchaseOrder sale = new tbl_PurchaseOrder()
            {
                PartyName = objpurchaseorder.PartyName,
                BillingName = objpurchaseorder.BillingName,
                ContactNo = objpurchaseorder.ContactNo,
                RemainingBal = objpurchaseorder.RemainingBal,
                Total = objpurchaseorder.Total,
                TransportName = objpurchaseorder.TransportName,
                DeliveryLocation = objpurchaseorder.DeliveryLocation,
                Deliverydate = objpurchaseorder.DeliveryDate,
                StateofSupply = objpurchaseorder.StateOfSupply,
                SGST = gst,
                CGST = gst,
                OrderDate = Convert.ToDateTime(objpurchaseorder.OrderDate),
                DueDate = objpurchaseorder.DueDate,
                Barcode = objpurchaseorder.Barcode,
                Status = objpurchaseorder.Status,
                VehicleNumber = objpurchaseorder.VehicleNumber

            };
            db.tbl_PurchaseOrders.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpurchaseorder.ListOfPurchaseOrder)
            {
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_PurchaseOrderInner inner = new tbl_PurchaseOrderInner()
                {

                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    OrderNo = sale.OrderNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    CGST = finalgsr,
                    SGST = finalgsr,
                    DiscountAmount = item.DiscountAmount,
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_PurchaseOrderInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });

        }



		[HttpPost]
		public ActionResult Dele(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
