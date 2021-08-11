using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankAdjustmentController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: BankAdjustment
		
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
		public ActionResult adjustdata()
		{
			var tb = db.tbl_BankAdjustmentselect("Select1", null, null, null, null, null, null, null,null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_BankAdjustmentselect("Details", id, null, null, null, null, null, null,null).Single(x => x.ID == id);
			return View(tb);
		}
        public JsonResult GetFruitName(string id)
        {
            return Json(GetFruitNameById(id), JsonRequestBehavior.AllowGet);
        }
        private static string GetFruitNameById(string id)
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT OpeningBal FROM tbl_BankAccount WHERE BankName = @Id");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    string name = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                    return name;
                }
            }
        }
        [HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{			
                tbl_BankAdjustment bt = new tbl_BankAdjustment();
                bt.ListOfAccounts = ListOfItems();
                return View(bt);          
		}
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_BankAccount where DeleteData='1'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["BankName"].ToString(),
                                Value = sdr["BankName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        [HttpPost]
		public ActionResult AddOrEdit(int id ,tbl_BankAdjustment conn)
		{
            try
            {
                db.tbl_BankAdjustmentselect("Insert", null, conn.BankAccount, conn.EntryType, conn.Amount, conn.Date, conn.Description, null,conn.Total);
                db.SubmitChanges();
                return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            finally {
                string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql;
                    sql = string.Format("Update tbl_BankAccount set OpeningBal=@openingbal WHERE BankName = @Id");
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", conn.BankAccount);
                        cmd.Parameters.AddWithValue("@openingbal", conn.Total);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_BankAdjustmentselect("Delete", id, null, null, null, null, null, null,null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public ActionResult AddOrEditUpdate(int id)
        {
            var tb = db.tbl_BankAdjustmentselect("Details", id, null, null, null, null, null, null,null).Single(x => x.ID == id);
            var vm = new tbl_BankAdjustment();
            vm.BankAccount = tb.BankAccount;
            vm.EntryType = tb.EntryType;
            vm.Amount = tb.Amount;
            vm.Date = Convert.ToDateTime(tb.Date);
            vm.Description = tb.Description;
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddOrEditUpdate(int id,tbl_BankAdjustment conn)
        {
            db.tbl_BankAdjustmentselect("Update", id, conn.BankAccount, conn.EntryType, conn.Amount, conn.Date, conn.Description, null,conn.Total);
            db.SubmitChanges();
            return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}