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
    public class CashFlowReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.CashFlow("Select", null, null, null).ToList();
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
            string Query = String.Format("(select InvoiceDate as InvoiceDate,PartyName,TableName as Type,Received as 'CashIn/CashOut' from tbl_saleinvoice where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1') union (select BillDate as InvoiceDate,PartyName,TableName as Type,Paid from tbl_purchaseBill where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1')");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "CashFlow");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/CashFlow.mrt"));
            report.RegData("CashFlow", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
    }
}