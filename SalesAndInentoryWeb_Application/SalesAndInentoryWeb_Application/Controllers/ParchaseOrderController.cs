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
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
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
                var tb = db.tbl_PurchaseOrderSelect("datetotdate", null, null, null, null, null, Convert.ToDateTime(date), Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_PurchaseOrderSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_PurchaseOrderSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).Single(x => x.OrderNo == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult AddOrder(int id = 0)
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
                                Discount = Convert.ToDouble(sdr["Discount"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where DeleteData='1' and Company_ID='" + MainLoginController.companyid1 + "'");
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
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1' and Company_ID='" + MainLoginController.companyid1 + "'");
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
                                Discount = Convert.ToDouble(sdr["Discount"]),
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
                VehicleNumber = objpurchaseorder.VehicleNumber,
                Company_ID = Convert.ToInt32(Session["UserId"]),
                DeleteData = Convert.ToBoolean(1),
                TableName=Convert.ToString("Purchase Order")
            };
            db.tbl_PurchaseOrders.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpurchaseorder.ListOfPurchaseOrder)
            {
                TempData["ID"] = sale.OrderNo;
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
                    Qty = item.Qty,
                    Company_ID = Convert.ToInt32(Session["UserId"]),
                    DeleteData = Convert.ToBoolean(1)

                };
                db.tbl_PurchaseOrderInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            //return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
            return RedirectToAction("GetReport");
        }

        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address,a.PhoneNo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.OrderNo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName,b.OrderDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.CGST, c.SGST,c.IGST,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseOrder as b,tbl_PurchaseOrderInner as c where b.OrderNo=" + id + " and c.OrderNo=" + id + " and a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and c.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1' and c.DeleteData='1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PurchaseOrder");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PurchaseOrderReport.mrt"));
            report.RegData("PurchaseOrder", dataSet);
            StiOptions.Viewer.Windows.Zoom = 0.5;
            return StiMvcViewer.GetReportResult(report);
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult ViewerEvent1()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult ReportAll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport1()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.OrderNo,b.PartyName,b.ContactNo,b.OrderDate,b.Total,b.Paid,b.RemainingBal,b.Status,b.DeleteData FROM tbl_CompanyMaster as a, tbl_PurchaseOrder as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData1 = '1' ");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PurchaseOrder");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PurchaseOrderDataReport.mrt"));
            report.RegData("PurchaseOrder", dataSet);
         
            return StiMvcViewer.GetReportResult(report);
        }
        [HttpPost]
        public ActionResult Dele(int id)
        {
            var tb = db.tbl_PurchaseOrderSelect("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Additem(tbl_ItemMaster item, int id = 0)
        {
            // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
            db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit, item.Discount);
            db.SubmitChanges();
            return RedirectToAction("AddOrder");
        }
        [HttpGet]
        public ActionResult Additem(int id = 0)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Addparty(int id = 0)
        {
                tbl_PartyMaster bt = new tbl_PartyMaster();
                bt.ListOfPartyGroup = ListOfItems();
                return View(bt);          
        }
        public string imagefile = null;
        [HttpPost]
        public ActionResult Addparty(IEnumerable<HttpPostedFileBase> files, tbl_PartyMaster party, int id = 0)
        {

            //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
            db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTType, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, Convert.ToInt32(Session["UserId"]), party.PaidStatus, imagefile);
            db.SubmitChanges();
            return RedirectToAction("AddOrder");
        }
    }
}
