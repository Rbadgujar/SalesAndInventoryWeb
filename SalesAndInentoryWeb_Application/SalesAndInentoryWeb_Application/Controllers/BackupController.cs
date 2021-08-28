using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

using System.Web.Mvc;
using System.Configuration;

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
        [HttpGet]
        public ActionResult backuptpdrive1()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            string backupDIR = "f:\\BackupDB";

            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }
            try
            {
                con.Open();
              SqlCommand  sqlcmd = new SqlCommand("backup database idealtec_inventory to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
                sqlcmd.ExecuteNonQuery();
            
              
            }
            catch (Exception ex)
            {
              
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