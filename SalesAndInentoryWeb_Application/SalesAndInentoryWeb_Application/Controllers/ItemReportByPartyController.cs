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
    public class ItemReportByPartyController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {
            tbl_DebitNote bt = new tbl_DebitNote();
            bt.ListOfParties = ListOfParties();
            return View(bt);
        }
        [HttpGet]
        public ActionResult Data(string paymenttype,string par)
        {
            if (par == "0")
            {
                var tb1 = db.ItemReportByParty("Select1", null, paymenttype, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.ItemReportByParty("Select", null, null, null, null,null, Convert.ToInt32(Session["UserId"])).ToList();
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
            string Query = string.Format("select a.TableName,a.PartyName,b.ItemName,b.Qty,b.ItemAmount from tbl_PurchaseBillInner as b,tbl_PurchaseBill as a where b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1'union ( select a.TableName,a.PartyName,b.ItemName,b.Qty,b.ItemAmount from tbl_SaleInvoiceInner as b,tbl_saleinvoice as a where b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1')");

            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "ItemReportByParty");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/ItemReportByParty.mrt"));
            report.RegData("ItemReportByParty", dataSet);
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