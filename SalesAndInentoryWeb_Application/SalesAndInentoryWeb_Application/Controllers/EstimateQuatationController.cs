using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Data;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class EstimateQuatationController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: EstimateQuatation
        public ActionResult Index()
        {
			return View();
        }

        [HttpGet]
        public ActionResult EstimateData(string date, string date2, string par)
        {
            if (par == "0")
            {
                //var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null, Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                var getdata1 = db.tbl_QuotationSelect("datetodate", null, null, null,Convert.ToDateTime(date),date2,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
                return Json(new { data = getdata1 }, JsonRequestBehavior.AllowGet);
            }
            var getdata = db.tbl_QuotationSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Detail(int id)
        //{
        //   // var tb = db.tbl_QuotationSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.RefNo == id);
        //   // return View(tb);
        //}

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            //    if (id == 0)
            //    {
            //        return View(new tblQuotation());
            //    }
            //    else
            //    {
            //var tb = db.tbl_QuotationSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x. == id);
            //var vm = new tblQuotation();
            ////ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
            ////UnusedAmount,Total,Status,image
            //vm.PartyName = tb.PartyName;
            //vm.BillingAddress = tb.BillingAddress;
            //vm.Date = Convert.ToDateTime(tb.Date);
            //vm.StateofSupply = tb.StateofSupply;
            //vm.ContactNo = tb.ContactNo;
            //vm.Description = tb.Description;
            ////vm.Image = tb.Image;
            //vm.Tax1 = tb.Tax1;
            //vm.TaxAmount1 = tb.TaxAmount1;
            //vm.CGST = tb.CGST;
            //vm.SGST = tb.SGST;
            //vm.TotalDiscount = tb.TotalDiscount;
            //vm.DiscountAmount1 = tb.DiscountAmount1;
            //vm.RoundFigure = tb.RoundFigure;
            //vm.Status = tb.Status;
            //vm.Total = tb.Total;
            //vm.Barcode = tb.Barcode;
            //vm.Itemcatgory = tb.ItemCategory;
            //vm.CalTotal = tb.CalTotal;
            //vm.TaxShow = tb.TaxShow;
            //vm.Discount = tb.Discount;
            //return View(vm);
            tblQuotation bt = new tblQuotation();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfParties = ListOfCategorys();
            return View(bt);

        }

        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.PartyName,b.BillingName,b.ContactNo,b.Company_ID,b.BillNo,b.PONo,b.Deliverydate,b.DeliveryLocation,b.TransportName,b.BillingName   , b.PoDate, b.DueDate, b.Tax1,  b.TaxAmount1,b.TotalDiscount,b.DiscountAmount1,b.Total,b.Paid,b.RemainingBal,c.ID,c.ItemName,c.BasicUnit,c.SaleTaxAmount,c.TaxForSale,c.ItemCode,c.SalePrice,c.Qty,c.freeQty,c.CGST, c.SGST,c.IGST,c.ItemAmount FROM tbl_CompanyMaster as a, tbl_PurchaseBill as b,tbl_PurchaseBillInner as c where b.BillNo=" + id + " and c.BillNo=" + id + " and a.CompanyID='1' and b.DeleteData1='1' and c.DeleteData1='1' ");
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,a.AddLogo,b.PartyName,b.BillingAddress,b.ContactNo, b.RefNo, b.Date, b.Tax1, b.TaxAmount1,b.TotalDiscount,b.BillingAddress,b.DiscountAmount1,b.Total,c.ID,c.ItemName,c.ItemCode,c.BasicUnit,c.SalePrice,c.Qty,c.freeQty,c.ItemAmount,c.TaxForSale,c.SaleTaxAmount,c.CGST,c.IGST,c.SGST FROM tbl_CompanyMaster  as a, tblQuotation as b,tbl_QuotationInner as c where b.RefNo="+id+" and c.RefNo="+id+" and a.CompanyID='" +MainLoginController.companyid1+ "' ");
            
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "Estimate");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/EstimateReport.mrt"));

            report.RegData("Estimate", dataSet);
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

        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where  Company_ID=" + MainLoginController.companyid1 + " and DeleteData='1'");
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
            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.CompanyID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1' and b.Company_ID = '" + MainLoginController.companyid1 + "'");
            string Query = string.Format("SELECT a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.PartyName,b.Date,b.Total,b.Status FROM tbl_CompanyMaster as a, tblQuotation as b where a.CompanyID='" +MainLoginController.companyid1 + "' and b.Company_ID='" +MainLoginController.companyid1+ "' and b.DeleteData = '1' ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "Estimate");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/EstimateHomeReport.mrt"));
            report.RegData("Estimate", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }



        private static List<SelectListItem> ListOfCategorys()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where  Company_ID=" + MainLoginController.companyid1 + " and DeleteData='1'");
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
        public ActionResult AddOrEdit(EstimateParty objEstimateDetails)
        {


            float gst = 0;
            float igst = 0;
         
            result = stateget(objEstimateDetails.StateOfSupply);
            if (result == 1)
            {
                float gstcount = (float)objEstimateDetails.TaxAmount1;
                gst = (float)(gstcount / 2);
            }
            else
            {
                igst = (float)objEstimateDetails.TaxAmount1;
            }
            //var gstcount = objEstimateDetails.TaxAmount1;
            //var gst = gstcount / 2;

            tblQuotation sale = new tblQuotation()
            {
                PartyName = objEstimateDetails.PartyName,
                Total = objEstimateDetails.Total,
                Status = objEstimateDetails.Status,
                StateofSupply = objEstimateDetails.StateOfSupply,
                Date = objEstimateDetails.Date,
                CGST=gst,
                SGST=gst,
                IGST=igst,
                DeleteData = Convert.ToBoolean(1),
                BillingAddress = objEstimateDetails.BillingAddress,
                Company_ID = Convert.ToInt32(Session["UserId"].ToString()),
                ContactNo = objEstimateDetails.ContactNo

            };
            db.tblQuotations.InsertOnSubmit(sale);
            db.SubmitChanges();
            TempData["ID"] = sale.RefNo;
            foreach (var item in objEstimateDetails.ListOfEstimateDetails)
            {
                //var gst1 = item.SaleTaxAmount;
                float finalgsr = 0;
                if (result == 1)
                {
                    float gst12 = (float)item.SaleTaxAmount;
                    finalgsr = (float)(gst12 / 2);
                }
                else
                {
                    igst = (float)item.SaleTaxAmount;
                }
                tbl_QuotationInner inner = new tbl_QuotationInner()
                {

 
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    RefNo = sale.RefNo,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty,
                    CGST=finalgsr,
                    SGST=finalgsr,
                    IGST=igst,
                    TaxForSale = item.TaxForSale,
                    DeleteData = Convert.ToBoolean(1),
                    SaleTaxAmount = item.SaleTaxAmount,
                    Company_ID = Convert.ToInt32(Session["UserId"].ToString()),
                    Discount = item.Discount,
                    DiscountAmount = item.DiscountAmount
                };
                db.tbl_QuotationInners.InsertOnSubmit(inner);
                db.SubmitChanges();

                //DataSet ds = new DataSet();
                //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo, a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.ID1, b.Date, b.ExpenseCategory, b.Paid,b.Balance,b.DeleteData,b.Status,b.Total,b.Company_ID,c.ID1,c.ItemName,c.SalePrice,c.Qty,c.ItemAmount,c.DeleteData,c.Company_ID FROM tbl_CompanyMaster  as a, tbl_Expenses as b,tbl_ExpensesInner as c where b.ID1='{0}' and c.ID1='{1}' and b.DeleteData='1' and c.DeleteData='1' ", sale.ID1, inner.ID1);
                //SqlDataAdapter SDA = new SqlDataAdapter(Query, constr);
                //SDA.Fill(ds);
                //string reportPath = Server.MapPath("~/Content/Report/ExpenceReport.mrt");
                //StiReport report = new StiReport();
                //report.Load(reportPath);

                //report.Compile();
                //StiPage page = report.Pages[0];
                //report.RegData("Expences", "Expences", ds.Tables[0]);

                //report.Dictionary.Synchronize();
                //report.Render();

            }

            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
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
                sql = string.Format("SELECT BillingAddress,ContactNo FROM tbl_PartyMaster WHERE Company_ID="+MainLoginController.companyid1+" and  PartyName = @Id");
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
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE Company_ID="+MainLoginController.companyid1+" and ItemName = @Id");
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
                                SalePrice =Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),                              
                                  SaleTaxAmount= Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),                               
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }
        //public ActionResult AddOrEdit(tbl_QuotationSelectResult esti, tbl_QuotationInnerspResult inner,int id=0)
        //{
        //    if (id == 0)
        //    {
        //        try
        //        {
        //            //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
        //           /// db.tbl_QuotationSelect("Insert", null, esti.PartyName, esti.BillingAddress, Convert.ToDateTime(esti.Date), esti.StateofSupply, esti.ContactNo, esti.Description, esti.Image, esti.Tax1, esti.TaxAmount1, esti.CGST, esti.SGST, esti.TotalDiscount, esti.DiscountAmount1, esti.RoundFigure, esti.Total, null, null, null, null, null, esti.Status, null, null, esti.Barcode, esti.Company_ID, esti.ItemCategory, esti.CalTotal, esti.TaxShow, esti.Discount);
        //            db.tbl_QuotationInnersp("Insert", null, inner.ItemID, inner.CategoryType, inner.ItemName, inner.BasicUnit, inner.ItemCode, inner.SalePrice, inner.TaxForSale, inner.SaleTaxAmount, inner.Qty, inner.freeQty, inner.ItemAmount, inner.Discount, inner.DiscountAmount, inner.Company_ID, inner.RefNo, inner.CGST, inner.SGST, inner.IGST, inner.CalTotal);
        //            db.SubmitChanges();
        //            return RedirectToAction("Index");
        //            //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception e)
        //        {
        //            return View("Error", new HandleErrorInfo(e, "EstimateQuatation", "AdOrEdit"));
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            //db.tbl_QuotationSelect("Update", null, esti.PartyName, esti.BillingAddress, Convert.ToDateTime(esti.Date), esti.StateofSupply, esti.ContactNo, esti.Description, esti.Image, esti.Tax1, esti.TaxAmount1, esti.CGST, esti.SGST, esti.TotalDiscount, esti.DiscountAmount1, esti.RoundFigure, esti.Total, null, null, null, null, null, esti.Status, null, null, esti.Barcode, esti.Company_ID, esti.ItemCategory, esti.CalTotal, esti.TaxShow, esti.Discount);
        //            db.tbl_QuotationInnersp("Update", null, inner.ItemID, inner.CategoryType, inner.ItemName, inner.BasicUnit, inner.ItemCode, inner.SalePrice, inner.TaxForSale, inner.SaleTaxAmount, inner.Qty, inner.freeQty, inner.ItemAmount, inner.Discount, inner.DiscountAmount, inner.Company_ID, inner.RefNo, inner.CGST, inner.SGST, inner.IGST, inner.CalTotal);
        //            db.SubmitChanges();
        //            return RedirectToAction("Index");
        //            //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_QuotationSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}