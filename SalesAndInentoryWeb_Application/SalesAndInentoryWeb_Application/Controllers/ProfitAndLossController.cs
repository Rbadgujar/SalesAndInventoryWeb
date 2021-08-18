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
    public class ProfitAndLossController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.ProfitOrLoss("Select", null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
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
            string Query = String.Format("select InvoiceDate as Date,TableName as Type,Received as 'Profit/Loss' from tbl_saleinvoice where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1' union select BillDate as Date,TableName as Type,Paid as 'Profit/Loss' from  tbl_PurchaseBill where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1' union select Date as Date,TableName as Type,Received as 'Profit/Loss'from  tbl_OtherIncome where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1' union select Date as Date,TableName as Type,Paid as 'Profit/Loss' from  tbl_Expenses where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "Profit&Loss");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/Profit&Loss.mrt"));
            report.RegData("Profit&Loss", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
    }
}