using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration;
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class PaymentOutController : Controller
	{

		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: PaymentOut
		public ActionResult Index()
		{
			return View();
		}
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult ReportAll()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult GetReport()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.Company_ID,b.CustomerName,b.ReceiptNo,b.PaymentType,b.Discount,b.Total,b.Paid,b.DeleteData FROM tbl_CompanyMaster as a, tbl_Paymentout as b where a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1' ");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PaymentOut");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PaymentOutDataReport.mrt"));
            report.RegData("PaymentOut", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1' and Company_ID='"+MainLoginController.companyid1+"'");
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
                                OpeningBal = Convert.ToInt32(sdr["OpeningBal"].ToString())

                            });
                        }
                    }
                    con.Close();
                }
            }
            return items2;
        }
        [HttpGet]
		public ActionResult paymentoutdata(string date, string date2, string par)
		{

            if (par == "0")
            {
                var tb = db.tbl_Paymentoutselect("datetodate", null,null,null, null,Convert.ToDateTime(date), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_Paymentoutselect("Select", null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_Paymentoutselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.Id == id);
			return View(tb);
		}
        public void paymenymagment(string name, int bal, int old)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                var final = old - bal;
                string sql1 = string.Format("update tbl_PartyMaster set OpeningBal=" + final + " where PartyName='" + name + "' and  DeleteData='1' and Company_ID=" + MainLoginController.companyid1 + "");
                SqlCommand cmd = new SqlCommand(sql1);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }
        [HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			    var bb = db.tbl_Paymentoutselect("RecipitNo", null, null, null, null, null, null, null, null, null, null, null, null, null).Single();
                var vm = new tbl_Paymentout();
                vm.ReceiptNo = Convert.ToInt32(bb.ReceiptNo) + 1;
                vm.ListOfParties = ListOfParties();

                return View(vm);			
		}
        [HttpGet]
        public ActionResult PaymentOutUpdate(int id = 0)
        {
            var tb = db.tbl_Paymentoutselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.Id == id);
            var vm = new tbl_Paymentout();
            vm.CustomerName = tb.CustomerName;
            vm.PaymentType = tb.PaymentType;
            vm.ReceiptNo = tb.ReceiptNo;
            vm.Description = tb.Description;
            vm.Paid = tb.Paid;
            vm.Discount = tb.Discount;
            vm.Total = tb.Total;
            vm.Status = tb.Status;
            vm.Date = Convert.ToDateTime(tb.Date);
            return View(vm);
        }
        [HttpPost]
        public ActionResult PaymentOutUpdate(tbl_Paymentout conn,int id = 0)
        {
            db.tbl_Paymentoutselect("Update", id, conn.CustomerName, conn.PaymentType, conn.ReceiptNo, conn.Date, conn.Description, conn.Paid, conn.Discount, conn.Total, conn.image, null, conn.Status, Convert.ToInt32(Session["UserId"]));
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
		public ActionResult AddOrEdit(tbl_Paymentout conn, int id=0)
		{
              paymenymagment(conn.CustomerName, Convert.ToInt32(conn.Paid), Convert.ToInt32(conn.TableName));
                db.tbl_Paymentoutselect("Insert", null, conn.CustomerName, conn.PaymentType, conn.ReceiptNo, conn.Date, conn.Description, conn.Paid, conn.Discount, conn.Total, conn.image, null, conn.Status, Convert.ToInt32(Session["UserId"]));
				db.SubmitChanges();
                return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
                //return RedirectToAction("Index");         
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
				var getdata = db.tbl_Paymentoutselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
				db.SubmitChanges();
			   return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);			
		}
        public ActionResult ViewerEvent1()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport1()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address,a.PhoneNo,a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo,b.ID,b.CustomerName,b.Date,b.PaymentType,b.Company_ID,b.Description,b.Paid from tbl_CompanyMaster as a,tbl_PaymentOut as b where b.ID="+id+" and a.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and a.DeleteData='1' and b.DeleteData='1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PaymentOut");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PaymentOut1.mrt"));
            report.RegData("PaymentOut", dataSet);
            StiOptions.Viewer.Windows.Zoom = 0.5;
            return StiMvcViewer.GetReportResult(report);
        }
        public ActionResult Report(int id = 0)
        {
            if (id != 0)
            {
                TempData["ID"] = id;
            }
            return View();
        }
    }
}