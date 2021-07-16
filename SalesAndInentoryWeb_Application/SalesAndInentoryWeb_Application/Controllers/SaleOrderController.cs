using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class SaleOrderController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleOrder
        public ActionResult SaleOrder()
        {
            return View();
        }
        public ActionResult daaa()
        {
            return View();
        }

        [HttpGet]
        public ActionResult showSaleOrder()
        {
            var getdata = db.tbl_SaleOrderSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(PartyDetails objpartydetails)
        {                                          
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var getdata = db.tbl_SaleOrderSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);           
        }
    }
}
