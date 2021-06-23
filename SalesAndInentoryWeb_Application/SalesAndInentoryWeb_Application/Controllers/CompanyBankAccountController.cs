using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CompanyBankAccountController : Controller
    {
        // GET: CompanyBankAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Data()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<CompanyBankAccount> party = db.CompanyBankAccounts.ToList<CompanyBankAccount>();
                return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new CompanyBankAccount());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.CompanyBankAccounts.Where(x => x.ID == id).FirstOrDefault<CompanyBankAccount>());
                }
            }
        }
    }
}