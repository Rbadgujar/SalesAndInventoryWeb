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
			DataSet ds=DA.show_data();
			List<tbl_BankAccount> tb = new List<tbl_BankAccount>();
			
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
		public ActionResult AddOrEdit()
		{		
			return View();
		}

		[HttpPost]
		public ActionResult AddOrEdit(FormCollection emp)
		{
			tbl_BankAccount reg = new tbl_BankAccount();			
			reg.BankName = emp["BankName"];
			reg.AccountName = emp["AccountName"];
			reg.AccountNo = emp["AccountNo"];
			reg.OpeningBal = float.Parse(emp["OpeningBal"]);
			reg.Date = emp["Date"];
			DA.InsertData(reg);
			TempData["msg"] = "Inserted";
			return View();			
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			DA.DeleteData(id);
			return View();
		}
	}
}

