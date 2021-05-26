﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.Data_Access;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankAccountController : Controller
    {
		Data_Access.Bank dblayer = new Data_Access.Bank();
		// GET: BankAccount
		public ActionResult Index()
		{			
			return View();
		}
		public ActionResult Data()
		{
			//using (idealtec_inventoryEntities6 db = new idealtec_inventoryEntities6())
			//{
			//	List<tbl_BankAccount> bank = db.tbl_BankAccount.ToList<tbl_BankAccount>();
			//	return Json(new { data = bank }, JsonRequestBehavior.AllowGet);


			//}  

			Bank b = new Bank();

			var data = b.SelectBank();

			//DataSet ds = dblayer.show_data();
			//ViewBag.Index = ds.Tables[0];
			return Json(new { data = data }, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
        public ActionResult AddOrEdit()
        {
			//public ActionResult AddOrEdit(int id = 0)
			//{
			//	//if (id == 0)
			//    return View(new tbl_BankAccount());
			//else
			//{
			//    using (idealtec_inventoryEntities3 db = new idealtec_inventoryEntities3())
			//    {
			//        return View(db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>());
			//    }
			//}
			return View();
			}

        [HttpPost]
        public ActionResult AddOrEdit(FormCollection bnk)
        {
			//public ActionResult AddOrEdit(tbl_BankAccount emp)
			//{

			//using (idealtec_inventoryEntities6 db = new idealtec_inventoryEntities6())
			//{
			//    if (emp.ID == 0)
			//    {
			//        db.tbl_BankAccount.Add(emp);
			//        db.SaveChanges();
			//        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			//    }
			//    else
			//    {
			//        db.Entry(emp).State = EntityState.Modified;
			//        db.SaveChanges();
			//        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
			//    }
			
			tbl_BankAccount reg = new tbl_BankAccount();
			//cust.C_id = custobj["C_id"];
			reg.BankName = bnk["BankName"];
			reg.AccountName = bnk["AccountName"];
			reg.AccountNo = bnk["AccountNo"];
			//reg.OpeningBal = bnk['OpeningBal'];
			reg.Date = bnk["Date"];
			dblayer.AddOrEdit(reg);
			TempData["msg"] = "Inserted";
			return View(); 

		}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (idealtec_inventoryEntities6 db = new idealtec_inventoryEntities6())
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