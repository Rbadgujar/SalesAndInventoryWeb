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
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: SaleInvoice
        //public ActionResult Index()
        //{
           
        //    return View();
        //}
        public ActionResult SaleIndexpage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult saleinvoiceshow()
        {
            return View();
        }

        public ActionResult daaa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
           var getdata = db.tbl_SaleInvoiceSelect("Delete", id,null,null,null,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
           db.SubmitChanges();
           return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
       
  
    }
}