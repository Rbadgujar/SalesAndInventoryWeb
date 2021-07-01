using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PaymentInController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: PaymentIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
               db.Configuration.LazyLoadingEnabled = false;
                List<tbl_PaymentIn> estimate = db.tbl_PaymentIn.ToList<tbl_PaymentIn>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PaymentInSelect("Select1",null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_PaymentIn());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_PaymentIn.Where(x => x.ID == id).FirstOrDefault<tbl_PaymentIn>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_PaymentIn emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.ID == 0)
                {
                    db.tbl_PaymentIn.Add(emp);
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
                tbl_PaymentIn emp = db.tbl_PaymentIn.Where(x => x.ID == id).FirstOrDefault<tbl_PaymentIn>();
                db.tbl_PaymentIn.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}