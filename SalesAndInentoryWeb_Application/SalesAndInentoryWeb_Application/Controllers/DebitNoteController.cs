using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DebitNoteController : Controller
    {
        // GET: DebitNote
        public ActionResult DebitNote()
        {
            return View();
        }

        public ActionResult DebitNoteData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_DebitNote> estimate = db.tbl_DebitNote.ToList<tbl_DebitNote>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_CreditNote1());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_CreditNote1.Where(x => x.InvoiceNo == id).FirstOrDefault<tbl_CreditNote1>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(tbl_DebitNote emp)
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                if (emp.InvoiceNo == 0)
                {
                    db.tbl_DebitNote.Add(emp);
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


        // GET: DebitNote/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DebitNote/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DebitNote/Create
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

        // GET: DebitNote/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DebitNote/Edit/5
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

        // GET: DebitNote/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DebitNote/Delete/5
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
