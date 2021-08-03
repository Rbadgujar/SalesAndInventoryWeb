using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BackupController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adr = new SqlDataAdapter();
        DataTable dt = new DataTable();
        // GET: Backup
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult backuptpdrive()
        {
             return View();
        }
        [HttpPost]
        public ActionResult backuptpdrive(string xyz)
        {
            con.ConnectionString= @"Data Source=103.83.81.80;Initial Catalog=idealtec_inventory;User ID=idealtec_inventory;Password=***********;MultipleActiveResultSets=True;Application Name=EntityFramework";
            string backdir ="";
            if(!System.IO.Directory.Exists(backdir))
            {
                System.IO.Directory.CreateDirectory(backdir);
            }
            try
            {
                con.Open();
                cmd = new SqlCommand("backup database test to disk '" + backdir + "\\" + DateTime.Now.ToString("ddmmyyyy_HHmmss") + ".Bak'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                //lblError.Text("Backup data successfully ");
            }
            catch(Exception ex)
            {
                //lblError.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
            }
            return View();

        }

        public ActionResult backupdatabase()
        {
            return View();                 
        }
        public ActionResult Restoredatabase()
        {
            return View();
        }
    }
}