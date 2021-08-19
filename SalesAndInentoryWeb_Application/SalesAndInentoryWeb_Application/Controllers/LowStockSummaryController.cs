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
    public class LowStockSummaryController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: OIncomeItemReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.LowStockSummary("LowStockSummary", null, null, null,null, Convert.ToInt32(Session["UserId"])).ToList();
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
            string Query = string.Format("select a.ItemName,a.OpeningQty,a.MinimumStock,a.atPrice,c.CompanyName,c.CompanyID,c.Address,c.PhoneNo,c.EmailID,c.AddLogo,c.GSTNumber from tbl_ItemMaster as a,tbl_CompanyMaster as c where a.OpeningQty >= a.MinimumStock and a.Company_ID='" + MainLoginController.companyid1 + "' and c.CompanyID='" + MainLoginController.companyid1 + "' and a.DeleteData='1' and c.DeleteData='1'");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "StockSummary");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/StockSummaryReport.mrt"));
            report.RegData("StockSummary", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
    }
}