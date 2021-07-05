using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PaymentInController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: PaymentIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_PaymentIn> estimate = db.tbl_PaymentIn.ToList<tbl_PaymentIn>();
                return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PaymentInSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

  
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(int id, tbl_PaymentInSelectResult pay)
        {
            try
            {
                db.tbl_PaymentInSelect("Insert", null, pay.PartyName, pay.PaymentType, pay.ReceiptNo, pay.Date, pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status,null,null);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
        [HttpPost]
        public ActionResult Delete(int id, tbl_PaymentInSelectResult pay)
        {
            try
            {
                var getdata = db.tbl_PaymentInSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}