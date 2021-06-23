using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            return View();
        }
    }
}