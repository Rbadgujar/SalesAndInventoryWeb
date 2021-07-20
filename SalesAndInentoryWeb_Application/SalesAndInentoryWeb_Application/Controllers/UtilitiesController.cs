using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using ClosedXML.Excel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class UtilitiesController : Controller
    {
      
        // GET: Utilities
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Importitem()
        {
            return View();
        }
        public ActionResult ImportParty()
        {
            return View();
        }
        // GET: Utilities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Utilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilities/Create
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

        // GET: Utilities/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Utilities/Edit/5
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

        // GET: Utilities/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Utilities/Delete/5
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
