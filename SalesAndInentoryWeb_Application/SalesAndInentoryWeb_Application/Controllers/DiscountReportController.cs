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
    public class DiscountReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.DiscountReport("Select", null, null, null,null, Convert.ToInt32(Session["UserId"])).ToList();
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
            string Query = String.Format("select TableName as Type,PartyName,Discount as 'SaleDiscount/PurchaseDiscount' from tbl_SaleInvoice where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1' union select TableName as Type,PartyName,Discount as 'SaleDiscount/PurchaseDiscount' from tbl_PurchaseBill where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("DiscountReport");
            adapter.Fill(dataSet, "DiscountReport");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/DiscountReport.mrt"));
            report.RegData("DiscountReport", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
    }
}