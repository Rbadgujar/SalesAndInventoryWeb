using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BankToBankController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();

		// GET: BankToBank
		public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
        public ActionResult GetData()
        {
			var tb = db.Banktobank("Select1", null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.Banktobank("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
			return View(tb);
		}

		[HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
			if (id == 0)
			{
				return View(new tbl_BanktoBankTransfer());
			}
			else
			{
				return View(db.Banktobank("Details", id, null, null, null, null, null, null).Where(x => x.ID == id));
				//return View(db.Banktobank.Where(x => x.ID == id).FirstOrDefault<tbl_BanktoBankTransfer>());
			}
        }

        [HttpPost]
        public ActionResult AddOrEdit(int id,tbl_BanktoBankTransfer emp)
        {
            
                if (emp.ID == 0)
                {
				    db.Banktobank("Insert", null, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, null);
                    db.SubmitChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
				db.Banktobank("Update", id, emp.FromBank, emp.ToBank, emp.Amount, emp.Date, emp.Descripition, null);
				db.SubmitChanges();
				   return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }

		[HttpPost]
		public ActionResult Delete(int id)
		{
				var tb = db.Banktobank("Delete", id, null, null, null, null, null, null).ToList();
				db.SubmitChanges();
				return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
	
    }
}