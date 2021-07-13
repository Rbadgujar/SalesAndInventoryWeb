﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
	public class PaymentOutController : Controller
	{

		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: PaymentOut
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult paymentoutdata()
		{
			var tb = db.tbl_Paymentoutselect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_Paymentoutselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.Id == id);
			return View(tb);
		}

		[HttpGet]
		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
				return View(new tbl_Paymentout());
			}
			else
			{
				var tb = db.tbl_Paymentoutselect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.Id == id);
				var vm = new tbl_Paymentout();
				vm.CustomerName = tb.CustomerName;
				vm.PaymentType = tb.PaymentType;
				vm.ReceiptNo = tb.ReceiptNo;
				vm.Description = tb.Description;
				vm.Paid = tb.Paid;
				vm.Discount = tb.Discount;
				vm.Total = tb.Total;
				vm.Status = tb.Status;
				vm.Date = Convert.ToDateTime(tb.Date);
				return View(vm);
				
			}
		}

		[HttpPost]
		public ActionResult AddOrEdit(tbl_Paymentout conn, int id=0)
		{
			if (id == 0)
			{

				db.tbl_Paymentoutselect("Insert", null, conn.CustomerName, conn.PaymentType, conn.ReceiptNo, conn.Date, conn.Description, conn.Paid, conn.Discount, conn.Total, conn.image, null, conn.Status, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
			else
			{
				db.tbl_Paymentoutselect("Update", id, conn.CustomerName, conn.PaymentType, conn.ReceiptNo, conn.Date, conn.Description, conn.Paid, conn.Discount, conn.Total, conn.image, null, conn.Status, null);
				db.SubmitChanges();
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
				var getdata = db.tbl_Paymentoutselect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
				db.SubmitChanges();
			   return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);			
		}
	}
}