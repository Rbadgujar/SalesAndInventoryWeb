using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ItemServicesController : Controller
    {
        // GET: ItemServices
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddServices()
        {
            return View();
        }
    }
}