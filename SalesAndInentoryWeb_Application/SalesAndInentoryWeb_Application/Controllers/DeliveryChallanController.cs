using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DeliveryChallanController : Controller
    {
        // GET: DeliveryChallan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Data()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_DeliveryChallan> party = db.tbl_DeliveryChallan.ToList<tbl_DeliveryChallan>();
                return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            }
        }
      
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_DeliveryChallan());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_DeliveryChallan.Where(x => x.ChallanNo == id).FirstOrDefault<tbl_DeliveryChallan>());
                }
            }
        }
    }
}