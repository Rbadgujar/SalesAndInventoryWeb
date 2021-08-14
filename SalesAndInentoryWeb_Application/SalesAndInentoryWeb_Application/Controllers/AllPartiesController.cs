using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
using System.Configuration;
using System.Web.Mvc;
using Stimulsoft.Report.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class AllPartiesController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: AllParties
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data(string date, string date2, string par) 
        {
            if (par == "0")
            {
                var tb1 = db.Reports("datetodate", null,null,null,null,null,null,null, date, null,null,null, null, null, null, null,null ,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null, date2, null, null,null).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.Reports     ("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            string Query = String.Format("select TableName,PartyName, ContactNo,Received as 'Recived/Paid' from tbl_SaleInvoice  union all select TableName,PartyName,  ContactNo,Received as 'Recived/Paid'  from tbl_SaleOrder  union all select TableName,PartyName,  ContactNo,Paid as 'Recived/Paid' from tbl_PurchaseBill union all select TableName,PartyName, ContactNo,Paid as 'Recived/Paid'  from tbl_PurchaseOrder");


            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "AllParties");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/AllPartiesReport.mrt"));

            report.RegData("AllParties", dataSet);
            return StiMvcViewer.GetReportResult(report);


        }
        public ActionResult Report()
        {
            return View();
        }
    }
}