using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PartyStatementController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        public ActionResult Index()
        {
            tbl_DebitNote bt = new tbl_DebitNote();         
            bt.ListOfParties = ListOfParties();
            return View(bt);
        }
        [HttpGet]
        public ActionResult Data(string paymenttype,string par,string date,string date2)
        {
            if (par == "0")
            {
                var tb1 = db.PartyStatement("Select1", null, paymenttype, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            if (par == "1")
            {
                var tb1 = db.tbl_SaleInvoiceSelect("PartyStatement", null, null, null, null, null, Convert.ToDateTime(date), Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.PartyStatement("Select", null, null, null,null,null,null, Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
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
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("(select TableName, PartyName, InvoiceDate, Total, Received, RemainingBal, Status from tbl_CreditNote1 where DeleteData= '1' and Company_ID = '"+MainLoginController.companyid1+ "')union(select TableName, PartyName, InvoiceDate, Total, Received, RemainingBal, Status from tbl_DebitNote where DeleteData = '1' and Company_ID = '" + MainLoginController.companyid1 + "') union(select TableName, PartyName, InvoiceDate, Total, Received, RemainingBal, Status from tbl_DeliveryChallan where DeleteData = '1' and Company_ID = '" + MainLoginController.companyid1 + "')union(select TableName, PartyName, BillDate as InvoiceDate, Total, Paid, RemainingBal, Status from tbl_PurchaseBill where DeleteData = '1' and Company_ID = '" + MainLoginController.companyid1 + "')union(select TableName, PartyName, OrderDate As InvoiceDate, Total, Paid, RemainingBal, Status from tbl_PurchaseOrder where DeleteData = '1' and Company_ID = '" + MainLoginController.companyid1 + "')union(select TableName, PartyName, InvoiceDate, Total, Received, RemainingBal, Status from tbl_SaleInvoice where DeleteData = '1' and Company_ID = '" + MainLoginController.companyid1 + "')");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PartyStatement");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PartyStatement.mrt"));
            report.RegData("PartyStatement", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        public ActionResult Report(int id = 0)
        {         
            return View();
        }
    }
}