using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PurchaseBillController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: PurchaseBill
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
        public ActionResult Data()
        {
			var tb = db.tbl_PurchaseBillselect("Select1", null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}
		//[HttpGet]
		//public ActionResult AddOrEdit(int id = 0)
		//{
		//    if (id == 0)
		//        return View(new tbl_PurchaseBill());
		//    else
		//    {
		//        using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
		//        {
		//            return View(db.tbl_PurchaseBill.Where(x => x.BillNo == id).FirstOrDefault<tbl_PurchaseBill>());
		//        }
		//    }
		//}

			//[HttpPost]
			//public ActionResult AddOrEdit(tbl_PurchaseBill emp)
			//{
			//    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			//    {
			//        if (emp.BillNo == 0)
			//        {
			//            db.tbl_PurchaseBill.Add(emp);
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


			//}

		[HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_PurchaseBillselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
    }
}