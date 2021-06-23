using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data;
using System.Configuration;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class BankAccountController : Controller
	{
		Data_Access.Bank DA = new Data_Access.Bank();
		public ActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public JsonResult Data()
		{
			idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10();
			List<tbl_BankAccount> tb = new List<tbl_BankAccount>();
			db.Configuration.LazyLoadingEnabled = false;
			DataSet ds = new DataSet();

			ds = DA.show_data();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				tb.Add(new tbl_BankAccount
				{
					ID = Convert.ToInt32(dr["ID"]),
					AccountName = dr["AccountName"].ToString(),
					BankName = dr["BankName"].ToString(),
					AccountNo = dr["AccountNo"].ToString(),
					OpeningBal = float.Parse(dr["OpeningBal"].ToString()),
					Date = dr["Date"].ToString(),
					
				});
			}
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
				return View(new tbl_BankAccount());
			else
			{
				using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
				{
					return View(db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>());
				}
			}
		}

		[HttpPost]
		public ActionResult AddOrEdit(tbl_BankAccount emp)
		{
			using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			{
				if (emp.ID == 0)
				{
					db.tbl_BankAccount.Add(emp);
					db.SaveChanges();
					return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					db.Entry(emp).State = EntityState.Modified;
					db.SaveChanges();
					return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
				}
			}
		}
	[HttpPost]
	public ActionResult Delete(int id)
	{
		using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
		{
			tbl_BankAccount emp = db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>();
			db.tbl_BankAccount.Remove(emp);
			db.SaveChanges();
			return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
	public ActionResult BankToBank()
	{
		return View();
	}
	}
}