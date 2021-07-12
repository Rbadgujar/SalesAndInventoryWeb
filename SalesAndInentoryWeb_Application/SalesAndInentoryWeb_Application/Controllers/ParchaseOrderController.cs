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

		//[HttpGet]
		//public ActionResult AddOrEdit(int id = 0)
		//{
		//    if (id == 0)
		//        return View(new tbl_PurchaseOrder());
		//    else
		//    {
		//        using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
		//        {
		//            return View(db.tbl_PurchaseOrder.Where(x => x.OrderNo == id).FirstOrDefault<tbl_PurchaseOrder>());
		//        }
		//    }
		//}

		//[HttpPost]
		//public ActionResult AddOrEdit(tbl_PurchaseOrder emp)
		//{
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
		//    }


		//   }
		// GET: ParchaseOrder/Details/5
		//

		// GET: ParchaseOrder/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		//// POST: ParchaseOrder/Create
		//[HttpPost]
		//public ActionResult Create(FormCollection collection)
		//{
		//    try
		//    {
		//        // TODO: Add insert logic here

		//        return RedirectToAction("Index");
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: ParchaseOrder/Edit/5
		//public ActionResult Edit(int id)
		//{
		//    return View();
		//}

		//// POST: ParchaseOrder/Edit/5
		//[HttpPost]
		//public ActionResult Edit(int id, FormCollection collection)
		//{
		//    try
		//    {
		//        // TODO: Add update logic here

		//        return RedirectToAction("Index");
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: ParchaseOrder/Delete/5
		//public ActionResult Delete(int id)
		//{
		//    return View();
		//}

		// POST: ParchaseOrder/Delete/5
		[HttpPost]
		public ActionResult Dele(int id)
		{
			var tb = db.tbl_PurchaseOrderSelect("Delete", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}
