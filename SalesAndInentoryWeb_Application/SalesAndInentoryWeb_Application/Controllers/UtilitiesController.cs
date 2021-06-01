using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class UtilitiesController : Controller
    {
        // GET: Utilities
        public ActionResult ImportParty()
        {
            return View();
        }
        public ActionResult Importitem()
        {
            return View();
        }

       
    }
}