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
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: DeliveryChallan
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowChallanData()
        {
            var getdata = db.tbl_DeliveryChallanSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_DeliveryChallanSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrEdit()
        {
            return View();
        }
        [HttpPost]
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