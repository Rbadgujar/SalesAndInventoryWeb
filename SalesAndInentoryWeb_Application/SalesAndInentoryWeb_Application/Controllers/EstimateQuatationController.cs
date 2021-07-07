using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
  
    public class EstimateQuatationController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: EstimateQuatation
        public ActionResult Index()
        {
			

			return View();
        }
      [HttpGet]
        public ActionResult EstimateData()
        {


            try
            {
                var getdata = db.tbl_QuotationSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tblQuotation());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tblQuotations.Where(x => x.RefNo == id).FirstOrDefault<tblQuotation>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tblQuotation emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.RefNo == 0)
                {
                    db.tblQuotations.Add(emp);
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
                tblQuotation emp = db.tblQuotations.Where(x => x.RefNo == id).FirstOrDefault<tblQuotation>();
                db.tblQuotations.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}