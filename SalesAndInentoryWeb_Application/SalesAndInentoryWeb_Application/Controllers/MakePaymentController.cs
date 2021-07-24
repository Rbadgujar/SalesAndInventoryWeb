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
            var getdata = db.tbl_MakePaymentSelect("Select1", null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }


		[HttpGet]
		public ActionResult MakePayment(int id = 0)
		{
			if (id == 0)
			{
                tbl_MakePayment objbank = new tbl_MakePayment();
                objbank.ListOfAccounts = (from obj in db.tbl_LoanBanks
                                          where obj.DeleteData.Equals(1)
                                          select new SelectListItem
                                          {
                                              Text = obj.AccountName,

                                          });
                return View(objbank);
            }
			else
			{

				var tb = db.tbl_MakePaymentSelect("Details", id, null, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new tbl_MakePayment();
				vm.PrincipleAmount = tb.PrincipleAmount;
				vm.PaidFrom = tb.PaidFrom;
				vm.InterestAmount = tb.InterestAmount;
				vm.AccountName = tb.AccountName;
				vm.TotalAmount = tb.TotalAmount;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
			}
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
		public ActionResult MakePayment( tbl_MakePayment conn, int id=0)
		{
			
				db.tbl_MakePaymentSelect("Insert", null, conn.PrincipleAmount, conn.InterestAmount, conn.Date, conn.TotalAmount, conn.PaidFrom, conn.AccountName, null);
				db.SubmitChanges();
				ModelState.Clear();
				return RedirectToAction("Makepayment");			
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_MakePaymentSelect("Delete", id, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

	}
}