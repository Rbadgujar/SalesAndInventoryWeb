using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankAdjustmentController : Controller
    {
        // GET: BankAdjustment
        public ActionResult Index()
        {
            return View();
        }
		//public ActionResult BankData()
		//{
		//	//idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10();
		//	//var data = db.tbl_BankAdjustmentselect("Select",null,null,null,null,null,null,null);
		//	//return View(data.ToList());
		//	return View();
		//}
		[HttpGet]
		public ActionResult InsertCustomer()
		{
			return View();
		}
		[HttpPost]
		public JsonResult AddOrEdit(tbl_BankAdjustment collection)
		{
			
			idealtec_inventoryEntities10 dc = new idealtec_inventoryEntities10();
			tbl_BankAdjustment tb = dc.bankadjust("Insert", null, collection.BankAccount, collection.EntryType, collection.Amount, collection.Date, collection.Description, null).FirstOrDefault();
			return Json(tb);
			
		}
	}
}