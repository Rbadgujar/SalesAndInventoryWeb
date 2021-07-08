using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class StartpageController : Controller
    {
        // GET: Startpage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult registration()
        {
            return View();
        }
       
    }
}