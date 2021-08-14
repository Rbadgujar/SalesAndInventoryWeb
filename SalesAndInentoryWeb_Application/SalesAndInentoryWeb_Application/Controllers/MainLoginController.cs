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
        public ActionResult OtpVerification()
        {
            return View();
        }
        public ActionResult OTP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFruitName1(string name)
        {
            TempData["Data"] = name;
            return new RedirectResult(@"~\registration\");
        }
        public static int companyid1;
        [HttpPost]
        public JsonResult ValidateUser(string userid, string password)
        {

            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("SELECT * FROM tbl_CompanyMaster WHERE PhoneNo='" + userid + "'and Password='" + password + "' ");
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    companyid1 = Convert.ToInt32(reader[0].ToString());
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
