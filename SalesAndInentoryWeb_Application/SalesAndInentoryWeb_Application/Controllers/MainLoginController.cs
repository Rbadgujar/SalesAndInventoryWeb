using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class MainLoginController : Controller
    {
        // GET: MainLogin
        public ActionResult Index()
        {
            return View();
        }
        public static int companyid1;
        [HttpPost]
        public ActionResult checkpass(string userId, string pass)
        {
           
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("SELECT * FROM tbl_CompanyMaster WHERE PhoneNo='" + userId + "'and Password='" + pass + "' ");
                SqlCommand cmd = new SqlCommand(sql,con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    companyid1 = Convert.ToInt32(reader[0].ToString());
                    return RedirectToAction("Dashboard", "Home");
                }
                return View("Index");
                                      
            }
        }
    }
}
