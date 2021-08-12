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
        public ActionResult creditnotedata()
        {
           var getdata = db.tbl_CreditNote1Select("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddOrEdit(PartyDetailsCreditNote objcreditnote)
         {

            var gstcount = objcreditnote.TaxAmount1;
            var gst = gstcount / 2;

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
                //Barcode = objcreditnote.Barcode,
                Status = objcreditnote.Status,
                VehicleNumber = objcreditnote.VehicleNumber,
                PONumber = objcreditnote.PONumber,
                PODate = Convert.ToDateTime(objcreditnote.PODate),
                Received = objcreditnote.Received
            };
            db.tbl_CreditNote1s.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objcreditnote.ListOfCreditNote)
            {
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_CreditNoteInner inner = new tbl_CreditNoteInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ReturnNo = sale.ReturnNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount,
                    SGST=finalgsr,
                    CGST=finalgsr,                   
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
