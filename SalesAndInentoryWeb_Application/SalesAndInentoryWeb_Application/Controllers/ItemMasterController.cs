using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ItemMasterController : Controller
    {
        // idealtec_inventoryEntities10 ew = new idealtec_inventoryEntities10();
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: ItemMaster
        public ActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public ActionResult Data()
        {

            var getdata = db.tbl_CompanyMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult itemdata()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_ItemMaster> estimate = db.tbl_ItemMaster.ToList<tbl_ItemMaster>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_ItemMaster());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_ItemMaster.Where(x => x.ItemID == id).FirstOrDefault<tbl_ItemMaster>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_ItemMaster emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.ItemID == 0)
                {
                    db.tbl_ItemMaster.Add(emp);
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


        // GET: ItemMaster/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemMaster/Create
        public ActionResult Create()
        {
            return View("ItemMaster");
        }

        // POST: ItemMaster/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemMaster/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemMaster/Delete/5
        public ActionResult Delete(int id)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                tbl_ItemMaster emp = db.tbl_ItemMaster.Where(x => x.ItemID == id).FirstOrDefault<tbl_ItemMaster>();
                db.tbl_ItemMaster.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: ItemMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
    }
}
