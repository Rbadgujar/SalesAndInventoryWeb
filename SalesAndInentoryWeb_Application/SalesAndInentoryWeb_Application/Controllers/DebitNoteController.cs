
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
                var tb = db.tbl_DebitNoteSelect("datetodate", null, null, null, null, null, null,Convert.ToDateTime(date),Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_DebitNoteSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_DebitNoteSelect("Details", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ReturnNo == id);
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
                Barcode = objdebitnote.Barcode,
                Status = objdebitnote.Status,
                VehicleNumber = objdebitnote.VehicleNumber,
                PONumber=objdebitnote.PONumber,
                PODate= Convert.ToDateTime(objdebitnote.PODate),
                Received=objdebitnote.Received             
            };
            db.tbl_DebitNotes.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objdebitnote.ListOfDebitNote)
            {
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
                    SaleTaxAmount = item.SaleTaxAmount,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_DebitNoteInners.InsertOnSubmit(inner);

                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });

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
			var tb = db.tbl_DebitNoteSelect("Delete", null, id,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ComboBoxPartial()
        {
            return PartialView("_ComboBoxPartial");
        }
    }
}
