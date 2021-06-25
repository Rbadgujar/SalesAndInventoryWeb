using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BarcodeController : Controller
    {
        // GET: Barcode
        public ActionResult index()
        {
            return View();
        }
        public ActionResult BarcodePrint()
        {
            return View();
        }
    }
}