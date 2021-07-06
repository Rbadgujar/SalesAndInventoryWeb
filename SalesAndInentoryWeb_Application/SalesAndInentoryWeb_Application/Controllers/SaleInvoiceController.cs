using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;


namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleInvoiceController : Controller
    {
        // GET: SaleInvoice
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult saleinvoicedata()
        {
            var tb = db.tbl_ExpensesSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaleIndexpage()
        {
            return View();
        }
                   public ActionResult SaleInvoice()
        {
           
            return View();
        }
    }
}