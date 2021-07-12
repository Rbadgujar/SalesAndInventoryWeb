using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ParchaseOrderController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: ParchaseOrder
		public ActionResult PurchaseOrder()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Purchaseorderdata()
        {
			var tb = db.tbl_PurchaseOrderSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

	[HttpGet]
		public ActionResult AddOrder()
		{
			//    if (id == 0)
			//        return View(new tbl_PurchaseOrder());
			//    else
			//    {
			//        using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			//        {
			//            return View(db.tbl_PurchaseOrder.Where(x => x.OrderNo == id).FirstOrDefault<tbl_PurchaseOrder>());
			//        }
			//    }
			return View();
		}

		[HttpPost]
		public ActionResult AddOrder(tbl_PurchaseOrder emp)
		{
			//    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			//    {
			//        if (emp.OrderNo == 0)
			//        {
			//            db.tbl_PurchaseOrder.Add(emp);
			//            db.SaveChanges();
			//            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
			//        }
			//        else
			//        {
			//            db.Entry(emp).State = EntityState.Modified;
			//            db.SaveChanges();
			//            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
			//        }
			return View();
		  }


	
		[HttpPost]
		public ActionResult Dele(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
