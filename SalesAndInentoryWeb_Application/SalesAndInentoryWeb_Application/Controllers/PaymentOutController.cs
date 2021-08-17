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
        [HttpGet]
		public ActionResult paymentoutdata(string date, string date2, string par)
		{

            if (par == "0")
            {
                var tb = db.tbl_Paymentoutselect("datetodate", null,date2,null, null,Convert.ToDateTime(date), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
                return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            }
            var tb1 = db.tbl_Paymentoutselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_Paymentoutselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.Id == id);
			return View(tb);
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
	}
}