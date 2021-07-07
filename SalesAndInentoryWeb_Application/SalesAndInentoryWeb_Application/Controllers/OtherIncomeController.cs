using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
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
        public ActionResult LoadData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_OtherIncome> estimate = db.tbl_OtherIncome.ToList<tbl_OtherIncome>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_OtherIncome());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_OtherIncome.Where(x => x.Id == id).FirstOrDefault<tbl_OtherIncome>());
                }
            }
        }
        [HttpGet]
        public ActionResult ExpenceData()
        {
            var tb = db.tbl_OtherIncomeSelect("Select1",null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public ActionResult AddOrEdit(tbl_OtherIncome emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.Id == 0)
                {
                    db.tbl_OtherIncome.Add(emp);
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
                tbl_OtherIncome emp = db.tbl_OtherIncome.Where(x => x.Id == id).FirstOrDefault<tbl_OtherIncome>();
                db.tbl_OtherIncome.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}