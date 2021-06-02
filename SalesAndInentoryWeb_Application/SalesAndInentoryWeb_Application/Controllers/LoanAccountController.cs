using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class LoanAccountController : Controller
    {
        // GET: LoanAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddOrEdit()
        {
            return View();
        }
    }
}