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
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_SaleInvoice> saleinvoice = db.tbl_SaleInvoice.ToList<tbl_SaleInvoice>();
                return Json(new { data = saleinvoice }, JsonRequestBehavior.AllowGet);
            }
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