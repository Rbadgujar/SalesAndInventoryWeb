using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ExpenceController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: Expence
		public ActionResult Index()
        {
            return View();
        }

       [HttpGet]
       public ActionResult Expencescategory()
        {
            return View();
        }
		[HttpGet]
        public ActionResult ExpenceData()
        {
			var tb = db.tbl_ExpensesSelect("Select1", null, null, null, null, null, null,null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_Expenses());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_Expenses.Where(x => x.ID1 == id).FirstOrDefault<tbl_Expenses>());
                }
            }
        }

        //[HttpPost]
        //public ActionResult AddOrEdit(tbl_Expenses emp)
        //{
        //    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //    {
        //        if (emp.ID1 == 0)
        //        {
        //            db.tbl_Expenses.Add(emp);
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            db.Entry(emp).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }


        // }

        [HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.tbl_ExpensesSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
    }
}