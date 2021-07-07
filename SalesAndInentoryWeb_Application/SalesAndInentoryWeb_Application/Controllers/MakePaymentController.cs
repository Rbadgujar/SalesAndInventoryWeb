using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class MakePaymentController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: MakePayment
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Makepayment()
        {
            return View();
        }

       [HttpGet]
        public ActionResult makykdata()
        {

            var getdata = db.tbl_MakePaymentSelect("Select1", null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult maykpayment()
        {
            return View();
        }

    }
}