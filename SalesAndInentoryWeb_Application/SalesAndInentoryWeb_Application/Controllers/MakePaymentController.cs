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
    public class MakePaymentController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: MakePayment
        
		public ActionResult Makepayment()
		{
			return View();
		}
		[HttpGet]
        public ActionResult makykdata()
        {
            var getdata = db.tbl_MakePaymentSelect("Select", null, null, null, null, null, null, null, MainLoginController.companyid1, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }


		[HttpGet]
		public ActionResult MakePayment(int id = 0)
		{
            return View();
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
                sql = string.Format("SELECT LoanAmount FROM tbl_LoanBank WHERE AccountName = @Id");
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
		public ActionResult MakePaymentPopUp( tbl_MakePayment conn, int id=0)
		{
            try
            {
                db.tbl_MakePaymentSelect("Insert", null, conn.PrincipleAmount, conn.InterestAmount, conn.Date, conn.TotalAmount, conn.PaidFrom, conn.AccountName, MainLoginController.companyid1, conn.Total);
                db.SubmitChanges();
                ModelState.Clear();
                return RedirectToAction("Makepayment");
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
                    sql = string.Format("Update tbl_LoanBank set LoanAmount=@openingbal WHERE AccountName = @Id");
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Id", conn.AccountName);
                        cmd.Parameters.AddWithValue("@openingbal", conn.TotalAmount);
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
			var tb = db.tbl_MakePaymentSelect("Delete", id, null, null, null, null, null, null, MainLoginController.companyid1, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        [HttpGet]
        public ActionResult MakePaymentPopUp(int id=0)
        {
            
                tbl_MakePayment bt = new tbl_MakePayment();
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
                sql = string.Format("SELECT AccountName FROM tbl_LoanBank where Company_ID='"+ MainLoginController.companyid1 + "' DeleteData='1'");
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
                                Text = sdr["AccountName"].ToString(),
                                Value = sdr["AccountName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        [HttpGet]
        public ActionResult MakePaymentUpdate(int id=0)
        {
            var tb = db.tbl_MakePaymentSelect("Details", id, null, null, null, null, null, null, null,null).Single(x => x.ID == id);
            var vm = new tbl_MakePayment();
            vm.AccountName = tb.AccountName;
            vm.PrincipleAmount = tb.PrincipleAmount;
            vm.PaidFrom = tb.PaidFrom;
            vm.InterestAmount = tb.InterestAmount;
            vm.TotalAmount = tb.TotalAmount;
            vm.Date = Convert.ToDateTime(tb.Date);
            return View(vm);
        }
        [HttpPost]
        public ActionResult MakePaymentUpdate(int id,tbl_MakePayment conn)
        {
            db.tbl_MakePaymentSelect("Update", null, conn.PrincipleAmount, conn.InterestAmount, conn.Date, conn.TotalAmount, conn.PaidFrom, conn.AccountName, MainLoginController.companyid1, conn.Total);
            db.SubmitChanges();
            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}