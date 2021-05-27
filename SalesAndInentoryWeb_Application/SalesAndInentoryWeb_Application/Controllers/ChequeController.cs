using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ChequeController : Controller
    {
        // GET: Cheque
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChequeData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_DebitNote> bank = db.tbl_DebitNote.ToList<tbl_DebitNote>();
                return Json(new { data = bank }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}