using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class AllTransactionController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {
            tbl_DebitNote bt = new tbl_DebitNote();
            bt.ListOfParties = ListOfParties();
            return View(bt);
        }
        [HttpGet]
        public ActionResult Data(string par,string date,string date2,string paymenttype)
        {
            if (par == "0")
            {
                //var tb = db.tbl_PurchaseBillselect("datetodate", null, null, null, null, null, Convert.ToDateTime(date), null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                var getinvoice1 = db.tbl_SaleInvoiceSelect("AllTransaction", null, null, null, null, null, Convert.ToDateTime(date2), Convert.ToDateTime(date), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).ToList();
                return Json(new { data = getinvoice1 }, JsonRequestBehavior.AllowGet);
            }
            if (par == "1")
            {
                var tb1 = db.tbl_SaleInvoiceSelect("PaymentType", null, paymenttype, null, null, null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.AllTransaction("Select", null, null, null, null,null,null,null,Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("(select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_CreditNote1 where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')union (select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_DebitNote where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1') union  (select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_DeliveryChallan where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1' )union(select TableName,PartyName,BillDate as InvoiceDate,Total,Paid,RemainingBal,Status from tbl_PurchaseBill where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')union(select TableName,PartyName,OrderDate As InvoiceDate,Total,Paid as Received,RemainingBal,Status from tbl_PurchaseOrder where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')union(select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_SaleInvoice where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')union(select TableName,PartyName,OrderDate as InvoiceDate,Total,Received,RemainingBal,Status from tbl_SaleOrder where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "AllTransaction");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/AllTransactionDataReport.mrt"));
            report.RegData("AllTransaction", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        private static List<SelectListItem> ListOfParties()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_PartyMaster where DeleteData='1' and Company_ID='" + MainLoginController.companyid1 + "'");
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
    }
}