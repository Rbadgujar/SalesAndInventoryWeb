using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.SqlClient;
using System.Configuration;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CashInHandController : Controller
    {
		// GET: CashInHand
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		public ActionResult Index()
        {
            return View();

        }
		[HttpGet]
        public ActionResult Data()
        {
            
				var tb = db.tbl_CashAdjustmentselect("Select1",  null, null, null, null, null, null,null).ToList();
				return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_CashAdjustmentselect("Details", id,  null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{			
                tbl_CashAdjustment bt = new tbl_CashAdjustment();
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
        [HttpPost]
		public ActionResult AddOrEdit(int id, tbl_CashAdjustment emp)
		{

				db.tbl_CashAdjustmentselect("Insert", null, emp.CashAdjustment, emp.CashAmount,Convert.ToDateTime(emp.Date), emp.Description,emp.BankName,null);
				db.SubmitChanges();
				return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			

		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_CashAdjustmentselect("Delete", id, null, null, null, null, null,null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public ActionResult AddOrEditUpdate(int id)
        {
            var tb = db.tbl_CashAdjustmentselect("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
            var vm = new tbl_CashAdjustment();
            vm.CashAdjustment = tb.CashAdjustment;
            vm.CashAmount = tb.CashAmount;
            vm.Description = tb.Description;
            vm.BankName = tb.BankName;
            vm.Date = Convert.ToDateTime(tb.Date);
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddOrEditUpdate(int id,tbl_CashAdjustment emp)
        {
            db.tbl_CashAdjustmentselect("Update", id, emp.CashAdjustment, emp.CashAmount, Convert.ToDateTime(emp.Date), emp.Description, emp.BankName, null);
            db.SubmitChanges();
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}
