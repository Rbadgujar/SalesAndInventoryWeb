using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application.ViewModel;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class HomeController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Dashboard()
        //{
           
        //      return View();
        //}
        public string CompanyName;
        public ActionResult order()
        {

            DateTime myDateTime = DateTime.Now;
            string year = myDateTime.Year.ToString();
            var date = myDateTime.Year.ToString();
            var date1 = Convert.ToInt32(date) + 1;
            var date2 = "01-04-" + date + "";
            var date3 = "01-04-" + date1 + "";
            var tb = db.tbl_SaleInvoiceSelect("totalsalecount", null, null, null, null, null, Convert.ToDateTime(date3), Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).Single();
            var tb1 = db.tbl_PurchaseBillselect("totalsalecount1", null, null, null, null, null, Convert.ToDateTime(date2), Convert.ToDateTime(date3), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).Single();

            //var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb,data1=tb1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult todayrecord()
        {

            DateTime myDateTime = DateTime.Now;
        
            var date3 = myDateTime.Date.ToString("dd-MM-yyyy");
        
            var tb = db.tbl_PurchaseBillselect("todaycount", null, null, null, null, null, Convert.ToDateTime(date3), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).Single();
            var ww = db.tbl_PaymentInSelect("todaycount", null,null, null, null, Convert.ToDateTime(date3), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single();
            var tb1 = db.tbl_Paymentoutselect("todaycount", null, null, null, null, Convert.ToDateTime(date3), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single();
            var vc = db.tbl_SaleInvoiceSelect("todaycount", null, null, null, null, null,null, Convert.ToDateTime(date3), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).Single();
            //var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
         
            return Json(new { data = tb.Total,data1=ww.ReceivedAmount,date2=tb1.Paid,data3=vc.Total,data4= tb.Total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult salecount()
        {

            DateTime myDateTime = DateTime.Now;
            string year = myDateTime.Year.ToString();
            string mont = myDateTime.Month.ToString();
            var date = myDateTime.Year.ToString();
            var date1 = Convert.ToInt32(date) + 1;
            var date2 = "01-"+mont+"-" + date + "";
            var date3 = myDateTime.Date.ToString("dd-MM-yyyy");
            IList<tbl_SaleInvoice> FoodList = new List<tbl_SaleInvoice>();
         var vc = db.tbl_SaleInvoiceSelect("salecount", null, null, null, null, null, Convert.ToDateTime(date3), Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).ToList();
            //var tb = db.tbl_PurchaseBillselect("sum", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           
            return Json(new { data = vc }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dashboard()
        {
            tbl_CompanyMaster com = new tbl_CompanyMaster();
            string sql;          
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT CompanyName FROM tbl_CompanyMaster where DeleteData='1' and CompanyID='"+MainLoginController.companyid1+"'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                             com.CompanyName=sdr["CompanyName"].ToString();
                        }
                    }
                    con.Close();
                }
            }

            ViewBag.CompanyName = com.CompanyName;
            var ds = new Dashborddata();

           
            DateTime myDateTime = DateTime.Now;
            string year = myDateTime.Year.ToString();
            var date = myDateTime.Year.ToString();
            var date1 =Convert.ToInt32(date)+1;
            var date2 = "01-04-" + date + "";
            var date3 = "01-04-"+date1+"";
            var fd = db.tbl_SaleInvoiceSelect("totalsalecount", null, null, null, null, null, Convert.ToDateTime(date3), Convert.ToDateTime(date2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null, null, null, null).Single();
            var tb = db.tbl_PurchaseBillselect("totalsalecount1", null, null, null, null, null, Convert.ToDateTime(date2), Convert.ToDateTime(date3),null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null, null, null).Single();
            var ww = db.tbl_PaymentInSelect("totalsalecount2", null,date3, null, null,Convert.ToDateTime(date2), null, null, null, null, null, null, null, null).Single();
            var tb1 = db.tbl_Paymentoutselect("amuntcount", null,date3, null, null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single();
            var tb2 = db.tbl_Paymentoutselect("remingcount", null, null, null, null, Convert.ToDateTime(date2), null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single();

            ds.saleamount =(float)fd.Total;
            ds.salpending = (float)fd.RemainingBal; 
            ds.purchaseamount = (float)tb.Total;
            ds.purchasepending = (float)tb.RemainingBal;
            ds.PaymentIn = (float)ww.ReceivedAmount;
            ds.remaining = (float)ww.UnusedAmount;
            ds.paymnetout = (float)tb1.Paid;
            ds.remaining = (float)tb2.Paid;
            return View(ds);
        }
    }
}