using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class LoginPageController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        // GET: LoginPage
        [HttpGet]
        public ActionResult LoginPage()
        {
            
            return View();
        }
        void connectionstring()
        {
            con.ConnectionString = "Data Source=103.83.81.80;Initial Catalog=idealtec_inventory;User ID=idealtec_inventory;Password=***********;MultipleActiveResultSets=True;Application Name=EntityFramework";
        }

        public ActionResult LoginPage(Login model)
        {
            List<Login> loginlist = new List<Login>();
            string CS = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Login where Username = '" + model.Username + "' and Password= '" + model.Password + "' ", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var list = new Login();
                    list.id = Convert.ToInt32(rdr["id"]);
                }
            }
            con.Close();
            return View();
        }
             
        [HttpGet]
        public ActionResult signup()
        {

            return View();
        }
        [HttpPost]
        public ActionResult signup(Login model)
        {

            return View();
        }
    }
}