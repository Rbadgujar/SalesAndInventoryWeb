﻿using System;
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
    public class OtherIncomeController : Controller
    {

        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: OtherIncome
        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public ActionResult Data()
        {
			var tb = db.tbl_OtherIncomeSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_OtherIncomeSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.Id == id);
			return View(tb);
		}
        private static List<SelectListItem> ListOfAccount()
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
                                Value = sdr["ItemID"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
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

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
			if (id == 0)
			{

                tbl_OtherIncome bt = new tbl_OtherIncome();
                bt.ListOfAccounts = ListOfAccount();
                return View(bt);
              //  return View(new tbl_OtherIncome());
			}
			else
			{
				var tb = db.tbl_OtherIncomeSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.Id == id);
				var vm = new tbl_OtherIncome();
				vm.IncomeCategory = tb.IncomeCategory;
				vm.Date = Convert.ToDateTime(tb.Date);
				vm.total = tb.total;
				vm.Received = tb.Received;
				vm.Balance = tb.Balance;
				return View(vm);
			}
        }
        [HttpGet]
        public ActionResult otcategory()
		{
			return View();
		}
        [HttpPost]
        public ActionResult otcategory(tbl_otherIncomeCaategory emp)
        {      
              var tb = db.tbl_otherIncomeCategorySelect("Insert1", null,emp.OtherIncome,null);
             db.SubmitChanges();
            return RedirectToAction("Index");     
        }
        [HttpPost]
        public ActionResult AddOrEdit(tbl_OtherIncome emp,int id = 0)
        {
			if (id == 0)
			{
				var tb = db.tbl_OtherIncomeSelect("Insert",  null,emp.IncomeCategory, emp.Date, null, null, null, emp.RoundOFF, emp.total, emp.Received, emp.Balance, null, null, null, null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
			else
			{
				var tb = db.tbl_OtherIncomeSelect("Update", id, emp.IncomeCategory, emp.Date, null,null, null, emp.RoundOFF, emp.total, emp.Received, emp.Balance, null, null, null, null, null, null, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
	    }
				       
        [HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_OtherIncomeSelect("Delete", id,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}