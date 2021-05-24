﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankAccountController : Controller
    {
        // GET: BankAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (idealtec_inventoryEntities db = new idealtec_inventoryEntities())
            {
                List<tbl_BankAccount> empList = db.tbl_BankAccount.ToList<tbl_BankAccount>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_BankAccount());
            else
            {
                using (idealtec_inventoryEntities db = new idealtec_inventoryEntities())
                {
                    return View(db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_BankAccount emp)
        {
            using (idealtec_inventoryEntities db = new idealtec_inventoryEntities())
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
            using (idealtec_inventoryEntities db = new idealtec_inventoryEntities())
            {
                tbl_BankAccount emp = db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>();
                db.tbl_BankAccount.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}