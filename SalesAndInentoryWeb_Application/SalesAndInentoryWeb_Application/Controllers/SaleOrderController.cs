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
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleOrderController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleOrder
        public ActionResult SaleOrder()
        {

            return View();
        }
        [HttpPost]
       
        public ActionResult daaa()
        {
            return View();
        }
        public ActionResult Report(int id=0)        
        {
            if(id!=0)
            {
                TempData["ID"] = id;
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

            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo, b.OrderNo, b.OrderDate, b.DueDate, b.DeliveryLocation,b.Tax1, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit, c.CGST, c.SGST,c.IGST, c.SaleTaxAmount,c.SalePrice,c.Qty,c.TaxForSale,c.ItemAmount FROM tbl_CompanyMaster  as a, tbl_SaleOrder as b,tbl_SaleOrderInner as c where b.OrderNo="+id+" and c.OrderNo="+id+" and a.CompanyID="+MainLoginController.companyid1+" ");

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id + " and a.CompanyID="++" and b.DeleteData1='1' and c.DeleteData1='1' ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "SaleOrder");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/SaleOrderReport.mrt"));

            report.RegData("SaleOrder", dataSet);
            return StiMvcViewer.GetReportResult(report);

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
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.OrderNo,b.PartyName,b.OrderDate,b.DueDate,b.Total,b.Received,b.RemainingBal,b.Status,b.DeleteData FROM tbl_CompanyMaster as a, tbl_SaleOrder as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1' ");
            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "SaleOrder");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/SaleOrderData.mrt"));
            report.RegData("SaleOrder", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }






        [HttpGet]
        public ActionResult showSaleOrder(string date, string date2, string par)
        {
            if (par == "0")
            {
                var getdata1 = db.tbl_SaleOrderSelect("datetodate", null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2),null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = getdata1 }, JsonRequestBehavior.AllowGet);
            }
            var getdata = db.tbl_SaleOrderSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString()), null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            //if (id == 0)
            //{
            //    tbl_SaleOrder objbank = new tbl_SaleOrder();
            //    objbank.ListOfAccounts = (from obj in db.tbl_ItemMasters
            //                              select new SelectListItem
            //                              {
            //                                  Text = obj.ItemName,
            //                                  Value = obj.ItemID.ToString()
            //                              });
            //    return View(objbank);
            //}
            tbl_SaleOrder bt = new tbl_SaleOrder();
            bt.ListOfAccounts = ListOfAccount();
            bt.ListOfParties = ListOfParties();
            return View(bt);
        }

        public string Basicunit;
        public string stock(string ItemID)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql1 = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemName= @ItemID and Company_ID=" + MainLoginController.companyid1 + " and DeleteData='1' ");
                using (SqlCommand cmd = new SqlCommand(sql1))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {                     
                            Basicunit = sdr["Basicunit"].ToString();
                        }
                        sdr.Close();
                    }
                    con.Close();                 
                    return Basicunit;
                }

            }
        }
        private static List<SelectListItem> ListOfAccount()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where Company_ID=" + MainLoginController.companyid1 + " and  DeleteData='1'");
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
                sql = string.Format("SELECT * FROM tbl_PartyMaster where Company_ID=" + MainLoginController.companyid1 + " and DeleteData='1'");
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE PartyName = @Id and Company_ID=" + MainLoginController.companyid1 + "");
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE Barcode = @Id and Company_ID=" + MainLoginController.companyid1 + "");
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
        public int result;
        [HttpPost]
        public ActionResult AddOrEdit(PartyDetails objpartydetails)
        {
            float igst = 0;
            float gst = 0;
            float gstcount1;
            result = stateget(objpartydetails.StateOfSupply);
            if (result == 1)
            {
                gstcount1 = objpartydetails.TaxAmount1;
                gst = gstcount1 / 2;
            }
            else
            {
                igst = objpartydetails.TaxAmount1;
            }
            //var gstcount = objpartydetails.TaxAmount1;
            //var gst = gstcount / 2;

            tbl_SaleOrder sale = new tbl_SaleOrder()
            {
                PartyName = objpartydetails.PartyName,
                BillingName = objpartydetails.BillingName,
                ContactNo = objpartydetails.ContactNo,
                RemainingBal =Math.Round(objpartydetails.RemainingBal,2),
                Received= Math.Round(objpartydetails.Received,2),
                PaymentType=objpartydetails.PaymentType,
                Total = Math.Round(objpartydetails.CalTotal,2),
                TransportName = objpartydetails.TransportName,
                DeliveryLocation = objpartydetails.DeliveryLocation,
                Deliverydate = objpartydetails.DeliveryDate,
                Feild3 = objpartydetails.Feild3,
                StateofSupply = objpartydetails.StateOfSupply,
                SGST =Math.Round(gst,2),
                CGST = Math.Round(gst,2),
                IGST= Math.Round(igst,2),
                Company_ID = Convert.ToInt32(Session["UserId"].ToString()),
                OrderDate = Convert.ToDateTime(objpartydetails.OrderDate),
                DueDate = objpartydetails.DueDate,
                DeleteData = Convert.ToBoolean(1),
                Barcode = objpartydetails.Barcode,
                Status = objpartydetails.Status,
                VehicleNumber = objpartydetails.VehicleNumber
            };
            db.tbl_SaleOrders.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objpartydetails.listofitemdetail)
            {
                
                TempData["ID"] = sale.OrderNo;
                float finalgsr1 = 0;
                if (result == 1)
                {
                    float gst3 = (float)item.SaleTaxAmount;
                    finalgsr1 = gst3 / 2;
                }
                else
                {
                    igst = (float)item.SaleTaxAmount;
                }
                //var gst1 = item.SaleTaxAmount;
                //var finalgsr = gst1 / 2;
               Basicunit= stock(item.ItemName);
                tbl_SaleOrderInner inner = new tbl_SaleOrderInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    OrderNo = sale.OrderNo,
                    ItemID = item.ItemID,
                    CGST =Math.Round(finalgsr1,2),
                    SGST =Math.Round(finalgsr1,2),
                    IGST =Math.Round(igst,2),
                    TaxForSale=item.TaxForSale,
                    Company_ID= Convert.ToInt32(Session["UserId"].ToString()),
                    Discount=item.Discount,
                    DeleteData = Convert.ToBoolean(1),
                    BasicUnit=Basicunit,
                    DiscountAmount =item.DiscountAmount,
                    SaleTaxAmount=item.SaleTaxAmount,
                   ItemTotal=Math.Round(item.ItemTotal,2),
                    Qty=item.Qty
                };
                db.tbl_SaleOrderInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_SaleOrderSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);           
        }
        [HttpPost]
        public ActionResult Additem(tbl_ItemMaster item, int id = 0)
        {
            // ItemName,HSNCode ,BasicUnit,SecondaryUnit ,                      ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount ,TaxForPurchase ,                                                                  PurchasePrice,PurchaseTaxAmount ,OpeningQty,atPrice ,                           Date,ItemLocation,TrackingMRP,                      BatchNo,       SerialNo,    MFgdate,     Expdate,      Siz,     Description ,    MinimumStock,     Image1,     Barcode,Company_ID,Cess,saleTax,PurchaseTax,Profit
            db.tbl_ItemMasterSelect("Insert", null, item.ItemName, item.HSNCode, item.BasicUnit, item.SecondaryUnit, item.ItemCode, item.ItemCategory, item.SalePrice, item.TaxForSale, item.SaleTaxAmount, item.PurchasePrice, item.TaxForPurchase, item.PurchaseTaxAmount, item.PurchaseTaxAmount, item.OpeningQty, item.Date, item.atPrice, item.ItemLocation, item.TrackingMRP, item.BatchNo, item.SerialNo, item.MFgdate, item.Expdate, item.Size, item.Description, item.MinimumStock, item.Image1, null, null, null, null, null, null, item.Barcode, Convert.ToInt32(Session["UserId"].ToString()), null, null, item.Profit, item.Discount);
            db.SubmitChanges();
            return RedirectToAction("AddOrEdit");
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
            bt.ListOfPartyGroup = ListOfParties();
           
            return View(bt);
        }
        public string imagefile = null;
        [HttpPost]
        public ActionResult Addparty(IEnumerable<HttpPostedFileBase> files, tbl_PartyMaster party, int id = 0)
        {

            //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
            db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTType, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, Convert.ToInt32(Session["UserId"]), party.PaidStatus, imagefile);
            db.SubmitChanges();
            return RedirectToAction("AddOrEdit");
        }
    }
}
