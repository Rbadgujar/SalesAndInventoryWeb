using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class UnitHomepageController : Controller
    {
        // GET: UnitHomepage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddUnit()
        {
            return View();
        }
    }
}