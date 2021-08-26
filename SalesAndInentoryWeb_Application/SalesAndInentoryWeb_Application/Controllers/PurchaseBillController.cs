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
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
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
                var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null, Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            else {
                var tb1 = db.tbl_PurchaseBillselect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);

            }


        }

        [HttpGet]
        public ActionResult show(string date)
        {
            var tb1 = db.tbl_PurchaseBillselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Detail(int id)
		{
			var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).Single(x => x.BillNo == id);
			return View(tb);
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
            string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.CompanyID = '"+ MainLoginController.companyid1 + "' and  b.DeleteData = '1' and b.Company_ID = '" + MainLoginController.companyid1 + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PurchaseBillData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PurchaseBillData.mrt"));
            report.RegData("PurchaseBillData", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT ItemName FROM tbl_ItemMaster where DeleteData='1' and Company_ID='"+ MainLoginController.companyid1 + "'");
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
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1'  and Company_ID='" + MainLoginController.companyid1 + "'");
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
        public int openingstock, Minstock, minusamount;
        public void stock(int ItemID, int newqty)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql1 = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemID = @ItemID ");
                using (SqlCommand cmd = new SqlCommand(sql1))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            Minstock = Convert.ToInt32(sdr["MinimumStock"]);
                            openingstock = Convert.ToInt32(sdr["OpeningQty"].ToString());

                        }
                        sdr.Close();
                    }
                    con.Close();


                    int min = Minstock - newqty;
                    int opening = openingstock + newqty;

                    sotckcal(ItemID, min, opening);
                }

            }
        }

        public void sotckcal(int ItemID, int min, int opening)
        {

            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql1 = string.Format("Update tbl_ItemMaster set OpeningQty=" + opening + " where ItemID=" + ItemID + " ");
                SqlCommand cmd = new SqlCommand(sql1, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

        }
     
        public int OpeningBal1 = 0;
        public void partypaymnet(string pname, int Remaingbal, string Status, int totalbal)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("SELECT OpeningBal FROM tbl_PartyMaster WHERE PartyName =@Id AND  Company_ID=" + MainLoginController.companyid1 + " ");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", pname);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            OpeningBal1 = Convert.ToInt32(sdr["OpeningBal"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            if (Status == "Paid")
            {

                minusamount = OpeningBal1 + Remaingbal;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = string.Format("update tbl_PartyMaster Set OpeningBal=" + minusamount + " WHERE PartyName =@Id AND  Company_ID=" + MainLoginController.companyid1 + " ");
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", pname);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            else
            {

                minusamount = OpeningBal1 - totalbal;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = string.Format("update tbl_PartyMaster Set OpeningBal=" + minusamount + " WHERE PartyName =@Id AND  Company_ID=" + MainLoginController.companyid1 + " ");
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", pname);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }


        }
        string Comanystate;
        public int stateget(string state)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("Select  State from tbl_CompanyMaster where CompanyID=" + MainLoginController.companyid1 + "");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            Comanystate = sdr["State"].ToString();
                        }
                    }
                    con.Close();
                }
            }

            if (state == Comanystate)
            {
                return 1;
            }
            else
            {
                return 0;
            }


        }
        public int result;
        [HttpPost]
        public ActionResult AddPurchase(PartyDetailsForPurchase objpurchase)
        {

            float gst = 0;
            float igst = 0;
            float finalgsr = 0;
            result = stateget(objpurchase.StateOfSupply);
            if (result == 1)
            {
                float gstcount = (float)objpurchase.TaxAmount1;
                gst = (float)(gstcount / 2);
            }
            else
            {
                igst = (float)objpurchase.TaxAmount1;
            }
            partypaymnet(objpurchase.PartyName, Convert.ToInt32(objpurchase.RemainingBal), objpurchase.Status, Convert.ToInt32(objpurchase.Total));
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
                PaymentType=objpurchase.PaymentType,
                SGST = gst,
                CGST = gst,
                IGST = igst,
                Barcode = objpurchase.Barcode,
                Status = objpurchase.Status,
                VehicleNumber = objpurchase.VehicleNumber,
                Company_ID = Convert.ToInt32(Session["UserId"]),
                DeleteData = Convert.ToBoolean(1),
                TableName = Convert.ToString("Purchase Bill")
            };
            db.tbl_PurchaseBills.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpurchase.listpurchasedetail)
            {
                TempData["ID"] = sale.BillNo;
                stock(item.ItemID, item.Qty);
                if (result == 1)
                {
                    float gst1 = (float)item.SaleTaxAmount;
                    finalgsr = (float)(gst1 / 2);
                }
                else
                {
                    igst = (float)item.SaleTaxAmount;
                }
                tbl_PurchaseBillInner inner = new tbl_PurchaseBillInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    BillNo = sale.BillNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount,
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemID = item.ItemID,
                    CGST = finalgsr,
                    SGST = finalgsr,
                    IGST = igst,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty,
                    Company_ID = Convert.ToInt32(Session["UserId"]),
                    DeleteData = Convert.ToBoolean(1)
                };
                db.tbl_PurchaseBillInners.InsertOnSubmit(inner);
                db.SubmitChanges();
                TempData["msg"] = "Insert Data Sucessfully...";
            }
            //return Json(data:  new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet } );
            return RedirectToAction("GetReport");
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName = @Id and DeleteData='1' ");
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
                              
                                ItemID = Convert.ToInt32(sdr["ItemID"])
                                
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
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

            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id+ " and a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and c.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1' and c.DeleteData='1' ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "PurchaseBill");


            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/PurchaseBillReport.mrt"));

            report.RegData("PurchaseBill", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }
        public ActionResult Report(int id=0)
        {
            if (id != 0)
            {
                TempData["ID"] = id;
            }
            return View();
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

            var tb = db.tbl_PurchaseBillselect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
       [HttpPost]
        public ActionResult datewise(string m)
        {

            var tb = db.tbl_PurchaseBillselect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult order()
        {
             var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }


        //[HttpGet]
        //public ActionResult AddPurchaseUpdate(int id = 0)
        //{
        //    var tb = db.tbl_PurchaseBillselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.BillNo == id);
        //    var vm = new tbl_PurchaseBill();
        //    vm.BillNo = tb.BillNo;
        //    vm.PartyName = tb.PartyName;
        //    vm.BillingName = tb.BillingName;
        //    vm.ContactNo = tb.ContactNo;
        //    vm.BillDate = Convert.ToDateTime(tb.BillDate);
        //    vm.PoDate = Convert.ToDateTime(tb.PoDate);
        //    vm.DueDate = Convert.ToDateTime(tb.DueDate);
        //    vm.StateofSupply = tb.StateofSupply;
        //    vm.PaymentType = tb.PaymentType;
        //    vm.VehicleNumber = tb.VehicleNumber;
        //    vm.DeliveryLocation = tb.DeliveryLocation;
        //    vm.TransportName = tb.TransportName;
        //    vm.Deliverydate = Convert.ToDateTime(tb.Deliverydate);
        //    vm.Description = tb.Description;
        //    vm.TransportCharges = tb.TransportCharges;
        //    vm.Tax1 = tb.Tax1;
        //    vm.TaxAmount1 = tb.TaxAmount1;
        //    vm.CGST = tb.CGST;
        //    vm.SGST = tb.SGST;
        //    vm.Paid = tb.Paid;
        //    vm.DiscountAmount1 = tb.DiscountAmount1;
        //    vm.TotalDiscount = tb.TotalDiscount;
        //    vm.RoundFigure = tb.RoundFigure;
        //    vm.Total = tb.Total;
        //    vm.PaymentTerms = tb.PaymentTerms;
        //    vm.RemainingBal = tb.RemainingBal;
        //    vm.Status = tb.Status;
        //    vm.Barcode = tb.Barcode;
        //    vm.IGST = tb.IGST;
        //    vm.Feild4 = tb.Feild4;
        //    vm.Feild1 = tb.Feild1;
        //    return Json(GetFruitNameById5(id), JsonRequestBehavior.AllowGet);
         
        //}
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
                                Discount = Convert.ToDouble(sdr["Discount"]),
                                ItemID = Convert.ToInt32(sdr["ItemID"])
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
                                ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                Discount = Convert.ToDouble(sdr["Discount"]).ToString(),
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
			var tb = db.tbl_PurchaseBillselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        [HttpPost]
        public ActionResult Additem(tbl_ItemMaster item, int id = 0)
        {
            // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
            db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit, item.Discount);
            db.SubmitChanges();
            return RedirectToAction("AddPurchase");
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
            return RedirectToAction("AddPurchase");
        }
    }
}