using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data;
using System.Configuration;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class BankAccountController : Controller
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["idealtec_inventoryEntities10"].ConnectionString);
		//Data_Access.Bank dblayer = new Data_Access.Bank();
		// GET: BankAccount
		public ActionResult Index()
		{
			idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10();
			//var result = (from c in db.tbl_BankAccount select new );
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Action", "Select");
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return View();

			//using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			//{
			//	List<tbl_BankAccount> bank = db.tbl_BankAccount.ToList<tbl_BankAccount>();
			//	return Json(new { data = bank }, JsonRequestBehavior.AllowGet);
			//}
			//DataSet ds = dblayer.show_data();
			//ViewBag.cust = ds.Tables[0];
			//return View();
		}
	}
}
   //     public ActionResult Data()
   //     {
			//using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
			//{
			//	List<tbl_BankAccount> bank = db.tbl_BankAccount.ToList<tbl_BankAccount>();
			//	return Json(new { data = bank }, JsonRequestBehavior.AllowGet);
			//}
			////return View();
   //     }
//        [HttpGet]
//        public ActionResult AddOrEdit(int id = 0)
//        {
//            if (id == 0)
//                return View(new tbl_BankAccount());
//            else
//            {
//                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
//                {
//                    return View(db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>());
//                }
//            }
//        }

//        [HttpPost]
//        public ActionResult AddOrEdit(tbl_BankAccount emp)
//        {
//            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
//            {
//                if (emp.ID == 0)
//                {
//                    db.tbl_BankAccount.Add(emp);
//                    db.SaveChanges();
//                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
//                }
//                else
//                {
//                    db.Entry(emp).State = EntityState.Modified;
//                    db.SaveChanges();
//                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
//                }
//            }


//        }

//        [HttpPost]
//        public ActionResult Delete(int id)
//        {
//            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
//            {
//                tbl_BankAccount emp = db.tbl_BankAccount.Where(x => x.ID == id).FirstOrDefault<tbl_BankAccount>();
//                db.tbl_BankAccount.Remove(emp);
//                db.SaveChanges();
//                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult BankToBank()
//        {
//            return View();
//        }
//    }
//}