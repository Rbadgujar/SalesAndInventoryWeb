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
    public class BankToBankController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();

		// GET: BankToBank
		public ActionResult Index()
        {
            
            return View();
        }

		[HttpGet]
        public ActionResult GetData()
        {
			var tb = db.Banktobank("Select", null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.Banktobank("Details", id, null, null, null, null, null, null,null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {			
                tbl_BanktoBankTransfer bt = new tbl_BanktoBankTransfer();
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
                sql = string.Format("SELECT BankName FROM tbl_BankAccount where DeleteData='1' and Company_ID="+MainLoginController.companyid1+" ");
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
        public ActionResult AddOrEdit(int id,tbl_BanktoBankTransfer emp)
        {
            try
            {
                db.Banktobank("Insert", emp.ID, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, Convert.ToInt32(Session["UserId"]), emp.Total);
                db.SubmitChanges();
                ViewBag.meg = "Stored Suucessfully";
                return View("Index");
                //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql;
                    sql = string.Format("Update tbl_BankAccount set OpeningBal=@openingbal WHERE BankName = @Id");
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", emp.FromBank);
                        cmd.Parameters.AddWithValue("@openingbal",emp.Total);
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
				var tb = db.Banktobank("Delete", id, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null).ToList();
				db.SubmitChanges();
				return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public ActionResult AddOrEditMain(int id)
        {
            var tb = db.Banktobank("Details", id, null, null, null, null, null,Convert.ToInt32(Session["UserId"]), null).Single(x => x.ID == id);
            var vm = new tbl_BanktoBankTransfer();
            vm.FromBank = tb.FromBank;
            vm.ToBank = tb.ToBank;
            vm.Amount = tb.Amount;
            vm.Date = Convert.ToDateTime(tb.Date);
            vm.Descripition = tb.Descripition;
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddOrEditMain(int id,tbl_BanktoBankTransfer emp)
        {
            db.Banktobank("Update", id, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, Convert.ToInt32(Session["UserId"]), emp.Total);
            db.SubmitChanges();
            ViewBag.msg = "Stored Successfully";
            return View("Index");
            //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}