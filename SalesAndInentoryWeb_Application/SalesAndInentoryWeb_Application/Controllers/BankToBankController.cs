using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
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
			var tb = db.Banktobank("Select1", null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.Banktobank("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
			if (id == 0)
			{
                tbl_BanktoBankTransfer objbank = new tbl_BanktoBankTransfer();
                objbank.ListOfAccounts = (from obj in db.tbl_BankAccounts
                                          where obj.DeleteData.Equals(1)
                                          select new SelectListItem
                                          {
                                              Text = obj.BankName,
                                              Value = obj.ID.ToString(),

                                          });
                return View(objbank);
			}
			else
			{

				var tb = db.Banktobank("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new tbl_BanktoBankTransfer();
				vm.FromBank = tb.FromBank;
				vm.ToBank = tb.ToBank;
				vm.Amount = tb.Amount;
				vm.Date = Convert.ToDateTime(tb.Date);
     			vm.Descripition =tb.Descripition;
				return View(vm);
			}
		}
        public JsonResult GetFruitName(int id)
        {
            return Json(GetFruitNameById(id), JsonRequestBehavior.AllowGet);
        }
        private static string GetFruitNameById(int id)
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT OpeningBal FROM tbl_BankAccount WHERE ID = @Id");
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
        public ActionResult AddOrEdit(int id,tbl_BanktoBankTransfer emp,string string1,int total)
        {
            
                if (id == 0)
                {
                try
                {
                    db.Banktobank("Insert", null, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, null);
                    db.SubmitChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    
                      string sql;
                      string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                      using (SqlConnection con = new SqlConnection(constr))
                      {
                             sql = string.Format("Update tbl_BankAccount set OpeningBal=@Total FROM  WHERE BankName = @string");
                             using (SqlCommand cmd = new SqlCommand(sql))
                             {
                                  cmd.Connection = con;
                                  cmd.Parameters.AddWithValue("@string", string1);
                                  cmd.Parameters.AddWithValue("@Total", total);
                                  con.Open();
                                  cmd.ExecuteScalar();
                                  con.Close();

                             }
            

                       }
                }  
                }
                else
                {
				db.Banktobank("Update", id, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, null);
				db.SubmitChanges();
				   return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }

		[HttpPost]
		public ActionResult Delete(int id)
		{
				var tb = db.Banktobank("Delete", id, null, null, null, null, null, null).ToList();
				db.SubmitChanges();
				return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}      
    }
}