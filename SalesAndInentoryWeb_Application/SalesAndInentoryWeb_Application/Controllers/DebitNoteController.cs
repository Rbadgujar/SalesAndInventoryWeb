
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Configuration;
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DebitNoteController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: DebitNote
		public ActionResult DebitNote()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Debitdata(string date, string date2, string par)
        {
            if (par == "0")
            {
                var tb = db.tbl_DebitNoteSelect("datetodate", null, null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_DebitNoteSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }
        public int openingstock, Minstock;
        public string stock(int ItemID, int newqty)
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
                            unitofitem = sdr["BasicUnit"].ToString();
                        }
                        sdr.Close();
                    }
                    con.Close();


              
                    int opening = openingstock - newqty;

                    sotckcal(ItemID, opening);
                    return unitofitem;
                }

            }
        }
        public string unitofitem;
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
        public ActionResult Detail(int id)
		{
			var tb = db.tbl_DebitNoteSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).Single(x => x.ReturnNo == id);
			return View(tb);
		}

		[HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            tbl_DebitNote bt = new tbl_DebitNote();
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
                sql = string.Format("SELECT ItemName FROM tbl_ItemMaster where  Company_ID= '"+MainLoginController.companyid1+"' and DeleteData='1'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using ( SqlDataReader sdr = cmd.ExecuteReader())
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
                sql = string.Format("SELECT PartyName FROM tbl_PartyMaster where  Company_ID= '" + MainLoginController.companyid1 + "' and DeleteData='1'");
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
		public ActionResult AddOrEdit(PartyDetailsDebitNote objdebitnote)
		{
            var gstcount = objdebitnote.TaxAmount1;
            var gst = gstcount / 2;

            tbl_DebitNote sale = new tbl_DebitNote()
            {
                PartyName = objdebitnote.PartyName,
                BillingName = objdebitnote.BillingName,
                ContactNo = objdebitnote.ContactNo,
                RemainingBal = objdebitnote.RemainingBal,
                Total = objdebitnote.Total,
                TransportName = objdebitnote.TransportName,
                DeliveryLocation = objdebitnote.DeliveryLocation,
                Deliverydate = Convert.ToDateTime(objdebitnote.DeliveryDate),
                StateofSupply = objdebitnote.StateOfSupply,
                InvoiceDate = Convert.ToDateTime(objdebitnote.InvoiceDate),
                CGST = gst,
                SGST=gst,
                PaymentType = objdebitnote.PaymentType,
                Barcode = objdebitnote.Barcode,
                Status = objdebitnote.Status,
                VehicleNumber = objdebitnote.VehicleNumber,
                PONumber=objdebitnote.PONumber,
                PODate= Convert.ToDateTime(objdebitnote.PODate),
                Received=objdebitnote.Received  ,
                Company_ID= Convert.ToInt32(Session["UserId"]),
                DeleteData = Convert.ToBoolean(1),
                TableName=Convert.ToString("DebitNote")
            };
            db.tbl_DebitNotes.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objdebitnote.ListOfDebitNote)
            {
                TempData["ID"] = sale.ReturnNo;
                unitofitem=stock(item.ItemID, item.Qty);
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_DebitNoteInner inner = new tbl_DebitNoteInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ReturnNo = sale.ReturnNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount,
                    SGST = finalgsr,
                    CGST=finalgsr,           
                    BasicUnit= unitofitem,
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty,
                    Company_ID= Convert.ToInt32(Session["UserId"]),
                    DeleteData = Convert.ToBoolean(1)
                };
                db.tbl_DebitNoteInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            return RedirectToAction("GetReport");
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            // return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });

            //if (id == 0)
            //{

            //	var tb = db.tbl_DebitNoteSelect("Insert", emp .InvoiceNo, null, emp.PartyName, emp.BillingName, emp.PONumber, emp.PODate, emp.InvoiceDate, emp.DueDate, emp.StateofSupply,emp.ContactNo, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1,emp.TaxAmount1, emp.CGST, emp.SGST, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Received, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.IGST,null, null, null, null, null);
            //	db.SubmitChanges();

            //	return RedirectToAction("DebitNote");
            //}
            //else
            //{
            //	var tb = db.tbl_DebitNoteSelect("Update", emp.InvoiceNo, id, emp.PartyName, emp.BillingName, emp.PONumber, emp.PODate, emp.InvoiceDate, emp.DueDate, emp.StateofSupply, emp.ContactNo, emp.PaymentType, emp.TransportName, emp.DeliveryLocation, emp.VehicleNumber, emp.Deliverydate, emp.Description, emp.TransportCharges, null, emp.Tax1, emp.TaxAmount1, emp.CGST, emp.SGST, Convert.ToString(emp.TotalDiscount), emp.DiscountAmount1, emp.RoundFigure, emp.Total, emp.Received, emp.RemainingBal, emp.PaymentTerms, emp.Feild1, null, null, emp.Feild4, null, emp.Status, null, null, emp.Barcode, emp.IGST, null, null, null, null, null);
            //	db.SubmitChanges();
            //	return RedirectToAction("DebitNote");
            //}
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult ViewerEvent1()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address,a.AddLogo,a.GSTNumber, a.PhoneNo, a.EmailID,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo, b.ReturnNo, b.InvoiceDate, b.DeliveryLocation,b.DeliveryDate,b.DueDate, b.Tax1, b.CGST, b.SGST, b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Received,b.RemainingBal,c.ID,c.ItemName,c.ItemCode,c.BasicUnit,c.SalePrice,c.Qty,c.SaleTaxAmount,c.freeQty,c.ItemAmount,c.DeleteData FROM tbl_CompanyMaster  as a, tbl_DebitNote as b,tbl_DebitNoteInner as c where b.ReturnNo=" + id+ " and c.ReturnNo=" + id + " and b.Company_ID='"+ MainLoginController.companyid1 + "' and a.CompanyID='" + MainLoginController.companyid1 + "' and c.Company_ID='" + MainLoginController.companyid1 + "' and c.DeleteData='1' ");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "DebitNote");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/DebitNoteReport.mrt"));
            report.RegData("DebitNote", dataSet);
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
        public ActionResult ReportAll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport1()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.PartyName,b.DueDate,b.InvoiceDate,b.Total,b.ReturnNo,b.Received,b.RemainingBal,b.Status,b.DeleteData FROM tbl_CompanyMaster as a, tbl_DebitNote as b where a.CompanyID='"+ MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1' ");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "DebitNote");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/DebitNoteDataReport.mrt"));
            report.RegData("DebitNote", dataSet);
            return StiMvcViewer.GetReportResult(report);
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
        [HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_DebitNoteSelect("Delete", null, id,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ComboBoxPartial()
        {
            return PartialView("_ComboBoxPartial");
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
            return RedirectToAction("AddOrEdit");
        }
    }

}
