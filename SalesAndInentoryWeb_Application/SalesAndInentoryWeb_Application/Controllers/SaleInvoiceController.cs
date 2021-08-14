
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
using System.Configuration;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleInvoiceController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleInvoice
        //public ActionResult Index()
        //{
           
        //    return View();
        //}
        public ActionResult SaleIndexpage()
        {
            return View();
        }
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where Company_ID="+MainLoginController.companyid1+" and  DeleteData='1'");
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
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id + " and a.CompanyID='1' and b.DeleteData1='1' and c.DeleteData1='1' ");
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID, b.InvoiceID,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName, b.InvoiceDate, b.DueDate, b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.CGST, c.SGST,c.IGST,c.freeQty,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_SaleInvoice as b,tbl_SaleInvoiceInner as c where  b.InvoiceID=" + id+" and c.InvoiceID="+id+" ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "SaleInvoice");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/SaleReport.mrt"));

            report.RegData("SaleInvoice", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }
        public ActionResult Report()
        {
            return View();
        }

        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where  Company_ID="+ MainLoginController.companyid1 +" and  DeleteData='1' ");
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName =@Id AND  Company_ID=" + MainLoginController.companyid1 + " ");
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE Barcode =@Id and  Company_ID=" + MainLoginController.companyid1 + " AND DeleteData ='1' ");
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
                                ItemID=Convert.ToInt32(sdr["ItemID"].ToString()),
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                Discount = Convert.ToDouble(sdr["Discount"])
                               
                                //Discount = Convert.ToDouble(sdr["Discount"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemName =@Id and DeleteData='1' and Company_ID=" + MainLoginController.companyid1 + " ");
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
                                ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                Discount = Convert.ToDouble(sdr["Discount"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }
        [HttpGet]
        public ActionResult ShowInvoiceData()
        {
            var getinvoice = db.tbl_SaleInvoiceSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getinvoice }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaleInvoice()
        {
            tbl_SaleInvoice bt = new tbl_SaleInvoice();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfParties = ListOfParties();
            return View(bt);
        }
        public int openingstock,Minstock;

        public void stock(int ItemID,int newqty)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
               string sql1 = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemID = @ItemID and Company_ID=" + MainLoginController.companyid1 +" and DeleteData='1' ");
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
                    int opening = openingstock - newqty;

                    sotckcal(ItemID, min, opening);
                }
                
            }
        }

        public void sotckcal(int ItemID, int min ,int opening)
        {

            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql1 = string.Format("Update tbl_ItemMaster set OpeningQty="+opening+" where ItemID="+ItemID+" ");
                SqlCommand cmd = new SqlCommand(sql1,con);
                con.Open();
                cmd.ExecuteNonQuery();                                            
            }

        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult SaleInvoice(SaleInvoicePartyDetails objsalepartydetails)
      {
            var gstcount = objsalepartydetails.TaxAmount1;
            var gst = gstcount / 2;

            tbl_SaleInvoice sale = new tbl_SaleInvoice()
            {
                PartyName = objsalepartydetails.PartyName,
                BillingName = objsalepartydetails.BillingName,
                 ContactNo = objsalepartydetails.ContactNo,
                RemainingBal = objsalepartydetails.RemainingBal,
                CalTotal = objsalepartydetails.CalTotal,
                TransportName = objsalepartydetails.TransportName,
                DeliveryLocation = objsalepartydetails.DeliveryLocation,
                Deliverydate = objsalepartydetails.DeliveryDate,
                StateofSupply = objsalepartydetails.StateOfSupply,
                SGST = gst,
                CGST = gst,
                Company_ID =MainLoginController.companyid1,
                InvoiceDate = Convert.ToDateTime(objsalepartydetails.InvoiceDate),
                Barcode = objsalepartydetails.Barcode,
                Status = objsalepartydetails.Status,
                VehicleNumber = objsalepartydetails.VehicleNumber
            };
            db.tbl_SaleInvoices.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objsalepartydetails.SaleInvoiceItemDetails)
            {
                TempData["ID"] = sale.InvoiceID;
                stock(item.ItemID,item.Qty);
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_SaleInvoiceInner inner = new tbl_SaleInvoiceInner()
                {                   
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    ItemID = item.ItemID,
                    CGST = finalgsr,
                    SGST = finalgsr,
                    InvoiceID = sale.InvoiceID,
                    DiscountAmount = item.DiscountAmount,
                    Company_ID= Convert.ToInt32(MainLoginController.companyid1),
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_SaleInvoiceInners.InsertOnSubmit(inner);
                db.SubmitChanges();
            }

          
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
         
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
           var getdata = db.tbl_SaleInvoiceSelect("Delete", id,null,null,null,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           db.SubmitChanges();
           return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartPartial()
        {
            var model = new object[0];
            return PartialView("~/Views/DashBord/_ChartPartial.cshtml", model);
        }

        SalesAndInentoryWeb_Application.CompanyDataClassDataContext db1 = new SalesAndInentoryWeb_Application.CompanyDataClassDataContext();

        public ActionResult ChartPartial1()
        {
            var model = db1.tbl_SaleInvoices;
            return PartialView("~/Views/Home/_ChartPartial1.cshtml", model);
        }
    }
}