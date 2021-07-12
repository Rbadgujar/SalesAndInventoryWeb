﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class DebitNoteController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: DebitNote
		public ActionResult DebitNote()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Debitdata()
        {
			var tb = db.tbl_DebitNoteSelect("Select1", null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public ActionResult AddOrEdit()
        {
			//if (id == 0)
			//    return View(new tbl_DebitNote());
			return View();
           
        }

        //[HttpPost]
        //public ActionResult AddOrEdit(tbl_DebitNote emp)
        //{
        //    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //    {
        //        if (emp.InvoiceNo == 0)
        //        {
        //            db.tbl_DebitNote.Add(emp);
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            db.Entry(emp).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_DebitNoteSelect("Delete", null, id,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult ComboBoxPartial()
        {
            return PartialView("_ComboBoxPartial");
        }
    }
}
