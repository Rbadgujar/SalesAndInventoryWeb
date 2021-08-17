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

        public JsonResult frogetpass(string mono,string pass)
        {

            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("update tbl_CompanyMaster set [Password] = '"+pass+"' WHERE PhoneNo='" + mono + "'");
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
               int a= cmd.ExecuteNonQuery();
                if (a == 0)
                {

                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult mobilenocheek(string userid)
        {

            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string sql = string.Format("SELECT * FROM tbl_CompanyMaster WHERE PhoneNo='" + userid + "' ");
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult forgetpassword()
        {
            return View();
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
                    Session["UserId"] = reader[0].ToString();
                    companyid1 =Convert.ToInt32(Session["UserId"].ToString());
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
