using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ItemMasterController : Controller
    {
        // GET: ItemMaster
        public ActionResult Index()
        {
            return View();
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
            return View();
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
