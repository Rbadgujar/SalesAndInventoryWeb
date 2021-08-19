using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using Stimulsoft.Report.Mvc;
using System.Data;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PaymentInController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: PaymentIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData()
        {
            //using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;
            //    List<tbl_PaymentIn> estimate = db.tbl_PaymentIn.ToList<tbl_PaymentIn>();
            //    return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }


        [HttpGet]
        public ActionResult ShowData(string date, string date2, string par)
        {
            if (par == "0")
            {
                //var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null, Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                var getinvoice1 = db.tbl_PaymentInSelect("datetodate", null, null, null, null,Convert.ToDateTime(date),date2, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString())).ToList();
                return Json(new { data = getinvoice1 }, JsonRequestBehavior.AllowGet);
            }
            var getdata = db.tbl_PaymentInSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString())).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                var bb = db.tbl_PaymentInSelect("reciptno",null, null, null, null, null, null, null, null, null, null, null, null, null).Single();
                var vm = new tbl_PaymentIn();
                vm.ReceiptNo =Convert.ToInt32(bb.ReceiptNo);
                vm.ListOfParties = ListOfParties();
                return View(vm);
            }
            else
            {
                var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
                var vm = new tbl_PaymentIn();
                //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
                //UnusedAmount,Total,Status,image
                //vm.PartyName = tb.PartyName;
                vm.PaymentType = tb.PaymentType;
                vm.ReceiptNo = tb.ReceiptNo;
                           
                vm.Description = tb.Description;
                vm.ReceivedAmount = tb.ReceivedAmount;
                vm.UnusedAmount = tb.UnusedAmount;
                vm.Total = tb.Total;
                vm.Status = tb.Status;
                //vm.image = tb.image;
                return View(vm);
            }
        }



        [HttpGet]
        public ActionResult Updateview(int id)
        {
            var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
            var vm = new tbl_PaymentIn();
            //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
            //UnusedAmount,Total,Status,image
            vm.CustomerName = tb.PartyName;
            vm.PaymentType = tb.PaymentType;
            vm.ReceiptNo = tb.ReceiptNo;          
            vm.Description = tb.Description;
            vm.ReceivedAmount = tb.ReceivedAmount;
            vm.UnusedAmount = tb.UnusedAmount;
            vm.Total = tb.Total;
            vm.Status = tb.Status;
            //vm.image = tb.image;
            return View(vm);
        }
      public void paymentUpdate(int id,float newamount)
        {
            var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
            var prevalue = tb.ReceivedAmount;
            if(newamount>prevalue)
            {

            }

        }
        [HttpPost]
        public ActionResult Updateview(tbl_PaymentIn pay,int id)
        {



            db.tbl_PaymentInSelect("Update1", id, pay.CustomerName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
            db.SubmitChanges();
            return RedirectToAction("Index");
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
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.ReceiptNo,b.CustomerName,b.PaymentType,b.ReceivedAmount,b.UnusedAmount,b.Total,b.DeleteData FROM tbl_CompanyMaster as a, tbl_PaymentIn as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1+ "' and b.DeleteData='1' ");

            //string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.OrderNo,b.PartyName,b.OrderDate,b.DueDate,b.Total,b.Received,b.RemainingBal,b.Status,b.DeleteData FROM tbl_CompanyMaster as a, tbl_SaleOrder as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1' ");
            ////string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PaymentinData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PaymetinData.mrt"));
            report.RegData("PaymentinData", dataSet);
            return StiMvcViewer.GetReportResult(report);
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
                sql = string.Format("SELECT OpeningBal from tbl_PartyMaster WHERE PartyName =@Id and  Company_ID=" + MainLoginController.companyid1 + " ");
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
                                OpeningBal =Convert.ToInt32(sdr["OpeningBal"].ToString())
                                
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items2;
        }




        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1' and Company_ID="+MainLoginController.companyid1+"");
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
       
       

        public void  paymenymagment(string name,int bal,int old)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
              var final = old + bal;
              string  sql1 = string.Format("update tbl_PartyMaster set OpeningBal="+ final + " where PartyName='"+name+"' and  DeleteData='1' and Company_ID=" + MainLoginController.companyid1 + "");
                SqlCommand cmd = new SqlCommand(sql1);      
                    cmd.Connection = con;
                    con.Open();
                cmd.ExecuteNonQuery();
                                
            }
        }


        [HttpPost]
        public ActionResult AddOrEdit(tbl_PaymentIn pay, int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    paymenymagment(pay.CustomerName, Convert.ToInt32(pay.ReceivedAmount), Convert.ToInt32(pay.TableName));

                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Insert1", null, pay.CustomerName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null,Convert.ToInt32(Session["UserId"].ToString()));
                    db.SubmitChanges();
                    var bb = db.tbl_PaymentInSelect("reciptno", null, null, null, null, null, null, null, null, null, null, null, null, null).Single();

                    TempData["ID"] = bb.ReceiptNo;
                    return RedirectToAction("Report");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ew)
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    string pp = pay.CustomerName;
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Update1", id, pay.CustomerName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult Paymentin(int id = 0 )
        {
            if (id == 0)
            {
                return View(new tbl_PaymentIn());
            }
            else
            {
                var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
                var vm = new tbl_PaymentIn();
                //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
                //UnusedAmount,Total,Status,image
                //vm.PartyName = tb.PartyName;
                vm.PaymentType = tb.PaymentType;
                vm.ReceiptNo = tb.ReceiptNo;
                vm.Date =Convert.ToDateTime(tb.Date);
                vm.Description = tb.Description;
                vm.ReceivedAmount = tb.ReceivedAmount;
                vm.UnusedAmount = tb.UnusedAmount;
                vm.Total = tb.Total;
                vm.Status = tb.Status;
                //vm.image = tb.image;
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Paymentin( tbl_PaymentInSelectResult pay, int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Insert1",null, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, Convert.ToInt32(Session["UserId"].ToString()));
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Update1", id, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_PaymentInSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"].ToString())).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
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

            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.ReceiptNo,b.ID,b.CustomerName,b.PaymentType,b.ReceivedAmount,b.UnusedAmount,b.Total,b.DeleteData FROM tbl_CompanyMaster as a, tbl_PaymentIn as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1'and b.ID="+id+" ");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "Payin");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/PaymentInReport1.mrt"));

            report.RegData("Payin", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }

        public ActionResult Report(int Id = 0)
        {
            if (Id != 0)
            {
                TempData["ID"] = Id;
            }
            return View();
        }


    }
}