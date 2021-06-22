using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class demotController : Controller
    {
        // GET: demot
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_BanktoBankTransfer> empList = db.tbl_BanktoBankTransfer.ToList<tbl_BanktoBankTransfer>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_BanktoBankTransfer());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_BanktoBankTransfer.Where(x => x.ID == id).FirstOrDefault<tbl_BanktoBankTransfer>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_BanktoBankTransfer emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.ID == 0)
                {
                    db.tbl_BanktoBankTransfer.Add(emp);
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
                tbl_BanktoBankTransfer emp = db.tbl_BanktoBankTransfer.Where(x => x.ID == id).FirstOrDefault<tbl_BanktoBankTransfer>();
                db.tbl_BanktoBankTransfer.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}