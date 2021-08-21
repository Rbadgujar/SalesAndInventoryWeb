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
using System.Data;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CreditnoteController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Creditnote
        public ActionResult CreditNote()
        {
            return View();
        }

        //public ActionResult show()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult creditnotedata(string date, string date2, string par)
        {
            if (par == "0")
            {
                var getdata1 = db.tbl_CreditNote1Select("datetodate", null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null).ToList();
                return Json(new { data = getdata1 }, JsonRequestBehavior.AllowGet);
            }
            var getdata = db.tbl_CreditNote1Select("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

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
            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.InvoiceDate, b.InvoiceID, b.PartyName, b.PaymentType, b.Total, b.Received, b.RemainingBal,b.DeleteData, b.Status,b.Company_ID from tbl_SaleInvoice as b,tbl_CompanyMaster as c where b.Company_ID = '" + MainLoginController.companyid1 + "' and c.CompanyID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1'");
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.AddLogo,a.GSTNumber,b.InvoiceDate,b.PartyName,b.ReturnNo,b.Total,b.Received ,b.Company_ID, b.RemainingBal ,b.Status,b.DueDate FROM tbl_CompanyMaster as a, tbl_CreditNote1 as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID="+MainLoginController.companyid1+" and b.DeleteData='1' ");
            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData,b.Company_ID, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "CreditNoteData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/CreditNoteData.mrt"));
            report.RegData("CreditNoteData", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }


        public ActionResult Report(int Id=0)
        {
            if (Id != 0)
            {
                TempData["ID"] = Id;
            }
            return View();
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
            string Query = string.Format("SELECT a.AddLogo,a.GSTNumber,a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo, b.ReturnNo, b.InvoiceDate, b.DeliveryLocation,b.DeliveryDate,b.DueDate, b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.ItemCode,c.SalePrice,c.Qty,c.CGST, c.SGST,c.IGST,c.SaleTaxAmount,c.TaxForSale,c.ItemAmount FROM tbl_CompanyMaster  as a, tbl_CreditNote1 as b,tbl_CreditNoteInner as c where b.ReturnNo="+id+" and c.ReturnNo="+id+" and a.CompanyID="+MainLoginController.companyid1+" ");

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo, b.OrderNo, b.OrderDate, b.DueDate, b.DeliveryLocation,b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit, c.CGST, c.SGST,c.IGST, c.SaleTaxAmount,c.SalePrice,c.Qty,c.TaxForSale,c.ItemAmount FROM tbl_CompanyMaster  as a, tbl_SaleOrder as b,tbl_SaleOrderInner as c where b.OrderNo=" + id + " and c.OrderNo=" + id + " and a.CompanyID='1'");

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id + " and a.CompanyID='1' and b.DeleteData1='1' and c.DeleteData1='1' ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "CreditnoteDataset");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/CreditNote1.mrt"));

            report.RegData("CreditnoteDataset", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }

        [HttpPost]
        public ActionResult DeleteCredit(int id)
        {
            var getdata = db.tbl_CreditNote1Select("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            tbl_CreditNote1 bt = new tbl_CreditNote1();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfParties = ListOfParties();
            return View(bt);

        }

        public int openingstock, Minstock;
        public void stock(int ItemID, int newqty)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql1 = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemID = @ItemID");
                using (SqlCommand cmd = new SqlCommand(sql1))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            openingstock = Convert.ToInt32(sdr["OpeningQty"].ToString());

                        }
                        sdr.Close();
                    }
                    con.Close();


                  
                    int opening = openingstock + newqty;

                    sotckcal(ItemID, opening);
                }

            }
        }

        public void sotckcal(int ItemID, int opening)
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
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where Company_ID=" + MainLoginController.companyid1 + " and DeleteData='1'");
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
                sql = string.Format("SELECT * FROM tbl_PartyMaster where Company_ID=" + MainLoginController.companyid1 + " and  DeleteData='1'");
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
        public string Comanystate;
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
        public ActionResult AddOrEdit(PartyDetailsCreditNote objcreditnote)
         {

            float gst = 0;
            float igst = 0;
    
            result = stateget(objcreditnote.StateOfSupply);
            if (result == 1)
            {
                float gstcount = (float)objcreditnote.TaxAmount1;
                gst = (float)(gstcount / 2);
            }
            else
            {
                igst = (float)objcreditnote.TaxAmount1;
            }

            //var gstcount = objcreditnote.TaxAmount1;
            //var gst = gstcount / 2;

            tbl_CreditNote1 sale = new tbl_CreditNote1()
            {

                PartyName = objcreditnote.PartyName,
                BillingName = objcreditnote.BillingName,
                ContactNo = objcreditnote.ContactNo,
                RemainingBal = objcreditnote.RemainingBal,
                Total = objcreditnote.CalTotal,
                TransportName = objcreditnote.TransportName,
                DeliveryLocation = objcreditnote.DeliveryLocation,
                Deliverydate = objcreditnote.DeliveryDate,
                StateofSupply = objcreditnote.StateOfSupply,
                InvoiceDate = Convert.ToDateTime(objcreditnote.InvoiceDate),
                InvoiceNo= objcreditnote.InvoiceNo,
                CGST =gst,
                SGST=gst,
                //DueDate = Convert.ToDateTime(objcreditnote.DueDate),
                Company_ID= Convert.ToInt32(Session["UserId"].ToString()),
                //Barcode = objcreditnote.Barcode,
                Status = objcreditnote.Status,
                DeleteData=Convert.ToBoolean(1),
                VehicleNumber = objcreditnote.VehicleNumber,
                PONumber = objcreditnote.PONumber,
                PODate = Convert.ToDateTime(objcreditnote.PODate),
                Received = objcreditnote.Received
            };
            db.tbl_CreditNote1s.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objcreditnote.ListOfCreditNote)
            {
                float finalgsr = 0;
                float gst1 = 0;
                TempData["ID"] = sale.ReturnNo;
                if (result == 1)
                {
                     gst1 = (float)item.SaleTaxAmount;
                    finalgsr = (float)(gst1 / 2);
                }
                else
                {
                    igst = (float)item.SaleTaxAmount;
                }
                stock(item.ItemID, item.Qty);
                //var gst1 = item.SaleTaxAmount;
                //var finalgsr = gst1 / 2;
                tbl_CreditNoteInner inner = new tbl_CreditNoteInner()
                {
                     
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ReturnNo = sale.ReturnNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount,
                    SGST=finalgsr,
                    Company_ID= Convert.ToInt32(Session["UserId"].ToString()),
                    DeleteData = Convert.ToBoolean(1),
                    IGST=igst,
                    CGST =finalgsr,                   
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_CreditNoteInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });

        }
    }
}
