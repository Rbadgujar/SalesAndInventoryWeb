using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CreditnoteController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Creditnote
        public ActionResult CreditNote()
        {
            return View();
        }

        //public ActionResult show()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult creditnotedata()
        {
           var getdata = db.tbl_CreditNote1Select("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_CreditNote1Select("Delete", id,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrEdit()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_CreditNote1 emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.InvoiceNo == 0)
                {
                    db.tbl_CreditNote1.Add(emp);
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
    }
}
