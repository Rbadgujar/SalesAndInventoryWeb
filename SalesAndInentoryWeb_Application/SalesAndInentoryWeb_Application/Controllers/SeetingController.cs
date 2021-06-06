using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SeetingController : Controller
    {
        // GET: Seeting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenrailSeeting()
        {
            return View();
        }
        public ActionResult TransactionSeting()
        {
            return View();
        }
        public ActionResult PartySetting()
        {
            return View();
        }
        public ActionResult ItemSeeting()
        {
            return View();
        }
        public ActionResult taxGsstSetting()
        {
            return View();
        }

        // GET: Seeting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Seeting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seeting/Create
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

        // GET: Seeting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Seeting/Edit/5
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

        // GET: Seeting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Seeting/Delete/5
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
