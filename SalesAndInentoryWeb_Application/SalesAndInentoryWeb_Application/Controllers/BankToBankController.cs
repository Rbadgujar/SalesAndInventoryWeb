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
                tbl_BanktoBankTransfer objbank = new tbl_BanktoBankTransfer();
                objbank.ListOfAccounts = (from obj in db.tbl_BankAccounts
                                          where obj.DeleteData.Equals(1)
                                          select new SelectListItem
                                          {
                                              Text = obj.BankName,
                                              Value = obj.ID.ToString(),

                                          });
                return View(objbank);
			}
			else
			{

				var tb = db.Banktobank("Details", id, null, null, null, null, null, null).Single(x => x.ID == id);
				var vm = new tbl_BanktoBankTransfer();
				vm.FromBank = tb.FromBank;
				vm.ToBank = tb.ToBank;
				vm.Amount = tb.Amount;
				vm.Date = Convert.ToDateTime(tb.Date);
     			vm.Descripition =tb.Descripition;
				return View(vm);
			}
		}

        [HttpPost]
        public ActionResult AddOrEdit(int id,tbl_BanktoBankTransfer emp)
        {
            
                if (id == 0)
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