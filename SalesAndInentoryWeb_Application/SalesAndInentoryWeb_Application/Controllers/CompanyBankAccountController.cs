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
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: CompanyBankAccount
		public ActionResult Index()
        {
            return View();
        }
		[HttpGet]
        public ActionResult Data()
        {
			var tb = db.sp_CompanyBankAccount("Select1", null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		//[HttpGet]
		//public ActionResult AddOrEdit(int id = 0)
		//{
		//    if (id == 0)
		//        return View(new CompanyBankAccount());
		//    else
		//    {
		//        using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
		//        {
		//            return View(db.CompanyBankAccounts.Where(x => x.ID == id).FirstOrDefault<CompanyBankAccount>());
		//        }
		//    }
		//}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var tb = db.sp_CompanyBankAccount("Delete", id, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	}
}