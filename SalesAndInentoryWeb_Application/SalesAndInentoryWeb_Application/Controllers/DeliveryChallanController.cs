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
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DeliveryChallanController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: DeliveryChallan
        public ActionResult Index()
        {
            return View();
        }

        //'The DELETE statement conflicted with the REFERENCE constraint "FK__tbl_Deliv__Chall__68D28DBC". The conflict occurred in database "idealtec_inventory", table "dbo.tbl_DeliveryChallanInner", column 'ChallanNo'. The statement has been terminated.'

        [HttpGet]
        public ActionResult ShowChallanData(string date, string date2, string par)
        {
            if (par == "0")
            {
                //var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null, Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                var getdata1 = db.tbl_DeliveryChallanSelect("datetodate", null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null).ToList();
                return Json(new { data = getdata1 }, JsonRequestBehavior.AllowGet);
            }
            var getdata = db.tbl_DeliveryChallanSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString()), null, null, null).ToList();
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
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.ChallanNo,b.PartyName,b.BillingName,b.InvoiceDate,b.Total,b.Received,b.RemainingBal,b.DeleteData FROM tbl_CompanyMaster as a, tbl_DeliveryChallan as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1' ");

            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "DeliveryChallan");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/DeliveryChallanDataReport.mrt"));
            report.RegData("DeliveryChallan", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_DeliveryChallanSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            tbl_DeliveryChallan bt = new tbl_DeliveryChallan();
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster where DeleteData='1' and Company_ID="+MainLoginController.companyid1+"");
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
                sql = string.Format("SELECT * FROM tbl_PartyMaster where Company_ID="+MainLoginController.companyid1+" and DeleteData='1'");
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName = @Id and Company_ID="+MainLoginController.companyid1+"");
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

        [HttpGet]
        public ActionResult Dilaverychallen()
        {
            tbl_DeliveryChallan bt = new tbl_DeliveryChallan();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfParties = ListOfParties();
            return View(bt);
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemName = @Id and Company_ID="+MainLoginController.companyid1+"");
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
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id + " and a.CompanyID='1' and b.DeleteData1='1' and c.DeleteData1='1' ");
            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID, b.InvoiceID,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName, b.InvoiceDate, b.DueDate, b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.CGST, c.SGST,c.IGST,c.freeQty,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_SaleInvoice as b,tbl_SaleInvoiceInner as c where a.CompanyID=" + MainLoginController.companyid1 + " and b.InvoiceID=" + id + " and c.InvoiceID=" + id + " ");
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo, b.ChallanNo,b.Deliverydate,b.DeliveryLocation,b.TransportName ,b.BillingAddress  , b.Company_ID,b.InvoiceDate, b.DueDate, b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.ItemAmount,c.CalTotal, c.CGST, c.SGST,c.IGST FROM tbl_CompanyMaster as a, tbl_DeliveryChallan as b,tbl_DeliveryChallanInner as c where b.ChallanNo=" + id+" and c.ChallanNo="+id+" and c.Company_ID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1' and a.CompanyID='" + MainLoginController.companyid1 + "' ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "DeliveryChallan1");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/DeliveryChallan.mrt"));

            report.RegData("DeliveryChallan1", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult Report(int Id = 0)
        {
            if (Id != 0)
            {
                TempData["ID"] = Id;
            }
            return View();
        }

        public int result;
        [HttpPost]
        public ActionResult AddOrEdit(PartyDetailsDeliveryChallan objdeliverychallan)
        {

            float gst = 0;
            float igst = 0;        
            result = stateget(objdeliverychallan.StateOfSupply);
            if (result == 1)
            {
                float gstcount1 = (float)objdeliverychallan.TaxAmount1;
                gst = (float)(gstcount1 / 2);
            }
            else
            {
                igst = (float)objdeliverychallan.TaxAmount1;
            }
            //var gstcount = objdeliverychallan.TaxAmount1;
            //var gst = gstcount / 2;

            tbl_DeliveryChallan sale = new tbl_DeliveryChallan()
            {
                PartyName = objdeliverychallan.PartyName,
                BillingAddress = objdeliverychallan.BillingName,
                ContactNo = objdeliverychallan.ContactNo,
                RemainingBal = objdeliverychallan.RemainingBal,
                Total =Math.Round(objdeliverychallan.CalTotal,2),
                TransportName = objdeliverychallan.TransportName,
                PartyAddress=objdeliverychallan.BillingAddress,
                DeliveryLocation = objdeliverychallan.DeliveryLocation,
                Deliverydate = objdeliverychallan.DeliveryDate,
                CGST =Math.Round(gst,2),
                SGST =Math.Round(gst,2),
                IGST = Math.Round(igst,2),
                StateofSupply = objdeliverychallan.StateOfSupply,
                InvoiceDate = Convert.ToDateTime(objdeliverychallan.InvoiceDate),
                DueDate = objdeliverychallan.DueDate,
                Barcode = objdeliverychallan.Barcode,
                Description=objdeliverychallan.Description,
                Company_ID=Convert.ToInt32(Session["UserId"].ToString()),
                DeleteData= Convert.ToBoolean(1),
                Status = objdeliverychallan.Status,
                VehicleNumber = objdeliverychallan.VehicleNumber,            
                Received = objdeliverychallan.Received
            };
            db.tbl_DeliveryChallans.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objdeliverychallan.ListOfDeliveryChallan)
            {
                float finalgsr2 = 0;
                if (result == 1)
                {
                    float gst1 = (float)item.SaleTaxAmount;
                    finalgsr2 = (float)(gst1 / 2);
                }
                else
                {
                    igst = (float)item.SaleTaxAmount;
                }
                
                tbl_DeliveryChallanInner inner = new tbl_DeliveryChallanInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ChallanNo = sale.ChallanNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    SGST=Math.Round(finalgsr2,2),
                    CGST=Math.Round(finalgsr2,2),
                    IGST=Math.Round(igst,2),        
                    CalTotal =Math.Round(item.ItemTotal,2),
                    DeleteData =Convert.ToBoolean(1),
                    DiscountAmount = item.DiscountAmount,
                    Company_ID= Convert.ToInt32(Session["UserId"].ToString()),
                    SaleTaxAmount =Math.Round(item.SaleTaxAmount,2),
                    ItemAmount =Convert.ToInt32(item.ItemTotal),
                    Qty = item.Qty
                };
                db.tbl_DeliveryChallanInners.InsertOnSubmit(inner);

                db.SubmitChanges();
                TempData["ID"] = sale.ChallanNo;
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
            ////try
            ////{
            ////    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
            ////    db.tbl_DeliveryChallanSelect("Insert", null, challan.PartyName, challan.BillingName, challan.BillingAddress, challan.PartyAddress,Convert.ToDateTime(challan.InvoiceDate), challan.DueDate, challan.StateofSupply, challan.ContactNo, challan.PaymentType, challan.TransportName, challan.DeliveryLocation, challan.VehicleNumber, challan.Deliverydate, challan.Description, challan.TransportCharges, challan.Image, challan.Tax1, challan.TaxAmount1, challan.CGST, challan.SGST, Convert.ToString(challan.TotalDiscount), challan.DiscountAmount1, challan.RoundFigure, challan.Total, challan.Received, challan.RemainingBal, challan.PaymentTerms,null,null,null,null,null,null, challan.Status, null, challan.ItemCategory, challan.Barcode, challan.IGST, challan.Company_ID, challan.CalTotal, challan.TaxShow, null);
            ////    db.SubmitChanges();
            ////    return RedirectToAction("Index");
            //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            ////}
            ////catch (Exception e)
            ////{
            ////    return View("Error", new HandleErrorInfo(e, "DeliveryChallan", "AdOrEdit"));
            ////}
        }
    }
}