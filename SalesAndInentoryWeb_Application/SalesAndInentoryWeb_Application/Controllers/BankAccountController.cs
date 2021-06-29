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
		idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10();
		//CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		//Data_Access.Bank DA = new Data_Access.Bank();
		public ActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public JsonResult Data()
		{

			var tb = db.BankAccountSelect("Select",null,null,null,null,null,null,null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		//[HttpGet]
		//public ActionResult AddOrEdit()
		//{		
		//	return View();
		//}

		//[HttpPost]
		//public ActionResult AddOrEdit(FormCollection emp)
		//{
		//	tbl_BankAccount reg = new tbl_BankAccount();			
		//	reg.BankName = emp["BankName"];
		//	reg.AccountName = emp["AccountName"];
		//	reg.AccountNo = emp["AccountNo"];
		//	reg.OpeningBal = float.Parse(emp["OpeningBal"]);
		//	reg.Date = emp["Date"];
		//	DA.InsertData(reg);
		//	TempData["msg"] = "Inserted";
		//	return View();			
		//}

		//[HttpPost]
		//public ActionResult Delete(int id)
		//{
		//	DA.DeleteData(id);
		//	return View();
		//}
	}
}

