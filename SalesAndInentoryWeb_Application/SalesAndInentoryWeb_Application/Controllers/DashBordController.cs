using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DashBordController : Controller
    {
        // GET: DashBord
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashbord()
        {
            return View();
        }
    }
}