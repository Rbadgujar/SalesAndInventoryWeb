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
    public class LoanAccountController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: LoanAccount
		public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
		public ActionResult BankData()
		{
			var tb = db.tbl_LoanBankSelect("Select", null, null, null, null, null, null, null,null,null,null,null,null,null, MainLoginController.companyid1, null,null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_LoanBankSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
        {
			
				return View(new tbl_LoanBank());
			
        }
		[HttpPost]
			
		public ActionResult AddOrEditPopUp(tbl_LoanBank emp,int id=0)
		{
           
            if (id == 0)
            {
                db.tbl_LoanBankSelect("Insert", null, emp.AccountName, emp.AccountNo, emp.Description, emp.LendarBank, emp.FirmName, emp.CurrentBal, emp.BalAsOf, emp.LoanReceive, emp.Interest, emp.Duration, emp.ProcessingFees, emp.PaidBy, MainLoginController.companyid1, null, emp.Total);
                db.SubmitChanges();
                return RedirectToAction("Index");

            }

            else
            {
                db.tbl_LoanBankSelect("Update", id, emp.AccountName, emp.AccountNo, emp.Description, emp.LendarBank, emp.FirmName, emp.CurrentBal, emp.BalAsOf, emp.LoanReceive, emp.Interest, emp.Duration, emp.ProcessingFees, emp.PaidBy, MainLoginController.companyid1, null, emp.Total);
                db.SubmitChanges();
				return RedirectToAction("Index");
			}
        }

			[HttpPost]
		  public ActionResult Delete(int id)
		  {
			  var tb = db.tbl_LoanBankSelect("Delete", id, null, null, null, null, null,null,null,null,null,null,null,null, MainLoginController.companyid1, null,null).ToList();
			  db.SubmitChanges();
			  return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		  }

        [HttpGet]
        public ActionResult LoanStatement()
        {
            tbl_CashAdjustment bt = new tbl_CashAdjustment();
            bt.ListOfAccounts = ListOfCategorys();
            return View(bt);
        }
        private static List<SelectListItem> ListOfCategorys()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_LoanBank where DeleteData='1'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items1.Add(new SelectListItem
                            {
                                Text = sdr["AccountName"].ToString(),
                                Value = sdr["AccountName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
        }
        [HttpGet]
        public ActionResult AddOrEditPopUp(int id=0)
        {
            if (id == 0)
            {
                return View(new tbl_LoanBank());
            }
            else
            {
                var tb = db.tbl_LoanBankSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
                var vm = new tbl_LoanBank();
                vm.AccountName = tb.AccountName;
                vm.ProcessingFees = tb.ProcessingFees;
                vm.Description = tb.Description;
                vm.Total = tb.Total;
                vm.FirmName = tb.FirmName;
                vm.LoanAmount = tb.LoanAmount;
                vm.PaidBy = tb.PaidBy;
                vm.AccountNo = tb.AccountNo;
                vm.LendarBank = tb.LendarBank;
                vm.CurrentBal = tb.CurrentBal;
                vm.Interest = tb.Interest;
                vm.Duration = tb.Duration;
                vm.BalAsOf = tb.BalAsOf;
                return View(vm);
            }
           
        }
    }
}