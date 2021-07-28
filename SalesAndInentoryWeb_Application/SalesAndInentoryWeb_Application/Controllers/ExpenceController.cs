using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ExpenceController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: Expence
		public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Expencescategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Expencescategory(tbl_ExpenseCategory emp)
        {
            var tb = db.tbl_ExpenseCategorySelect("Insert1", null, emp.CategoryName, null);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult ExpenceData()
        { 
			var tb = db.tbl_ExpensesSelect("Select1", null, null, null, null, null, null,null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
    	}

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            tbl_Expense bt = new tbl_Expense();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfCategory = ListOfCategorys();
            return View(bt);               
        }
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster");
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
                                Text = sdr["ItemName"].ToString(),
                                Value = sdr["ItemName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        private static List<SelectListItem> ListOfCategorys()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ExpenseCategory");
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
                                Text = sdr["CategoryName"].ToString(),
                                Value = sdr["CategoryName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
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
                sql = string.Format("SELECT SalePrice FROM tbl_ItemMaster WHERE ItemID = @Id");
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
        public ActionResult AddOrEdit(OtherParties objExpensesDetails)
        {
            tbl_Expense sale = new tbl_Expense()
            {
                ExpenseCategory = objExpensesDetails.ExpenseCategory,
                Paid=objExpensesDetails.Paid,
                Balance=objExpensesDetails.Balance,
                Date=objExpensesDetails.Date             
            };
            db.tbl_Expenses.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objExpensesDetails.ListOfOtherIncomeDetails)
            {
                tbl_ExpensesInner inner = new tbl_ExpensesInner()
                {
                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ID1 = sale.ID1,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty
                };
                db.tbl_ExpensesInners.InsertOnSubmit(inner);
                db.SubmitChanges();
            }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tb = db.tbl_ExpensesSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}