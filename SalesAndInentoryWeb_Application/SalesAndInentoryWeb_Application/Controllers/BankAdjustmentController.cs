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
        public ActionResult BankData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_BankAdjustment> BankData = db.tbl_BankAdjustment.ToList<tbl_BankAdjustment>();
                return Json(new { data = BankData }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_BankAdjustment());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_BankAdjustment.Where(x => x.ID == id).FirstOrDefault<tbl_BankAdjustment>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_BankAdjustment emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.ID == 0)
                {
                    db.tbl_BankAdjustment.Add(emp);
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
                tbl_BankAdjustment emp = db.tbl_BankAdjustment.Where(x => x.ID == id).FirstOrDefault<tbl_BankAdjustment>();
                db.tbl_BankAdjustment.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}