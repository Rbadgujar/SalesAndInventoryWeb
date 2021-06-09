using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CashInHandController : Controller
    {
        // GET: CashInHand
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult Data()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_SaleInvoice> party = db.tbl_SaleInvoice.ToList<tbl_SaleInvoice>();
                return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_CashAdjustment());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_CashAdjustment.Where(x => x.ID == id).FirstOrDefault<tbl_CashAdjustment>());
                }
            }
        }
    }
}