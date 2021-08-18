using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using SalesAndInentoryWeb_Application;
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
using System.Configuration;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DayBookController : Controller
    {
        // GET: DayBook
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {         
            return View();
        }
        public ActionResult Daybook(string sortOrder, int? page, DateTime? datePicker)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            DateTime userSelectedDate = DateTime.Parse(Request["datePicker"]);

            var startDate = userSelectedDate.Date;


            var applications = from s in db.tbl_SaleInvoices
                               where s.InvoiceDate >= startDate
                               select s;

            return Json(new { data = applications }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Data(string date)
        {
            var tb = db.DayBookReport("Select", null, null, null, null, null, null, Convert.ToDateTime(date), Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Data1(string date)
        {
            TempData["ID"] = date;
            return RedirectToAction("GetReport");
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            string date =Convert.ToString(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            string Query = string.Format("(select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_CreditNote1 where InvoiceDate='" + date + "' and DeleteData='1')union (select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_DebitNote where InvoiceDate='" + date + "' and DeleteData='1') union  (select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_DeliveryChallan where InvoiceDate='" + date + "' and DeleteData='1' )union(select TableName,PartyName,BillDate  as InvoiceDate,Total,Paid,RemainingBal,Status from tbl_PurchaseBill where BillDate='" + date + "' and DeleteData='1')union(select TableName,PartyName,OrderDate As InvoiceDate,Total,Paid,RemainingBal,Status from tbl_PurchaseOrder where OrderDate='" + date + "' and DeleteData='1')union(select TableName,PartyName,InvoiceDate,Total,Received,RemainingBal,Status from tbl_SaleInvoice where InvoiceDate='" + date + "' and DeleteData='1')union(select TableName,PartyName,OrderDate as InvoiceDate,Total,Received,RemainingBal,Status from tbl_SaleOrder where OrderDate='" + date + "' and DeleteData='1')union(select TableName,IncomeCategory as PartyName,Date as InvoiceDate,Total,Received,Balance,Status from tbl_OtherIncome where Date='" + date + "' and DeleteData='1')union(select TableName,ExpenseCategory as PartyName,Date as InvoiceDate,Total,Paid,Balance,Status from tbl_Expenses where Date='" +date + "' and DeleteData='1')");


            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "DayBookSale");

            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/DayBookReport.mrt"));

            report.RegData("DayBookSale", dataSet);
            return StiMvcViewer.GetReportResult(report);
          

        }
        public ActionResult Report()
        {
            return View();
        }

    }
}