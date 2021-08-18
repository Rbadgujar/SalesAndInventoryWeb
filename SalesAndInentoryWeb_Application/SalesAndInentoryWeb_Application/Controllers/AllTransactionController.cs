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
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
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
    }
}