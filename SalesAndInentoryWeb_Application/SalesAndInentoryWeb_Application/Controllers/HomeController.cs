using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application;

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
            Session["CompanyName"] = com.CompanyName;
            return View();
        }
    }
}