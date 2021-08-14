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
        public ActionResult ShowChallanData()
        {
            var getdata = db.tbl_DeliveryChallanSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddOrEdit(PartyDetailsDeliveryChallan objdeliverychallan)
        {
            var gstcount = objdeliverychallan.TaxAmount1;
            var gst = gstcount / 2;

            tbl_DeliveryChallan sale = new tbl_DeliveryChallan()
            {
                PartyName = objdeliverychallan.PartyName,
                BillingName = objdeliverychallan.BillingName,
                ContactNo = objdeliverychallan.ContactNo,
                RemainingBal = objdeliverychallan.RemainingBal,                
                Total = objdeliverychallan.CalTotal,
                TransportName = objdeliverychallan.TransportName,
                DeliveryLocation = objdeliverychallan.DeliveryLocation,
                Deliverydate = objdeliverychallan.DeliveryDate,
                CGST = gst,
                SGST=gst,
                StateofSupply = objdeliverychallan.StateOfSupply,
                InvoiceDate = Convert.ToDateTime(objdeliverychallan.InvoiceDate),
                DueDate = objdeliverychallan.DueDate,
                Barcode = objdeliverychallan.Barcode,
                Description=objdeliverychallan.Description,
                Status = objdeliverychallan.Status,
                VehicleNumber = objdeliverychallan.VehicleNumber,            
                Received = objdeliverychallan.Received
            };
            db.tbl_DeliveryChallans.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objdeliverychallan.ListOfDeliveryChallan)
            {
                var gst1 = item.SaleTaxAmount;
                var finalgsr = gst1 / 2;
                tbl_DeliveryChallanInner inner = new tbl_DeliveryChallanInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ChallanNo = sale.ChallanNo,
                    TaxForSale = item.TaxForSale,
                    Discount = item.Discount,
                    SGST=finalgsr,
                    CGST=finalgsr,
                    DiscountAmount = item.DiscountAmount,
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_DeliveryChallanInners.InsertOnSubmit(inner);

                db.SubmitChanges();
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