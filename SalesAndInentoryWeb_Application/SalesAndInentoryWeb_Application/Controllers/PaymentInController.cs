using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PaymentInController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: PaymentIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadData()
        {
            //using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;
            //    List<tbl_PaymentIn> estimate = db.tbl_PaymentIn.ToList<tbl_PaymentIn>();
            //    return Json(new { data = estimate }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }


        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PaymentInSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                var bb = db.tbl_PaymentInSelect("reciptno",null, null, null, null, null, null, null, null, null, null, null, null, null).Single();
                var vm = new tbl_PaymentIn();
                vm.ReceiptNo =Convert.ToInt32(bb.ReceiptNo)+1;
                return View(vm);
            }
            else
            {
                var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
                var vm = new tbl_PaymentIn();
                //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
                //UnusedAmount,Total,Status,image
                //vm.PartyName = tb.PartyName;
                vm.PaymentType = tb.PaymentType;
                vm.ReceiptNo = tb.ReceiptNo;
                vm.Date = tb.Date
                    ;
                vm.Description = tb.Description;
                vm.ReceivedAmount = tb.ReceivedAmount;
                vm.UnusedAmount = tb.UnusedAmount;
                vm.Total = tb.Total;
                vm.Status = tb.Status;
                //vm.image = tb.image;
                return View(vm);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(tbl_PaymentInSelectResult pay, int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Insert1", null, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    string pp = pay.PartyName;
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Update1", id, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult Paymentin(int id = 0 )
        {
            if (id == 0)
            {
                return View(new tbl_PaymentIn());
            }
            else
            {
                var tb = db.tbl_PaymentInSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.ID == id);
                var vm = new tbl_PaymentIn();
                //ID,CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount
                //UnusedAmount,Total,Status,image
                //vm.PartyName = tb.PartyName;
                vm.PaymentType = tb.PaymentType;
                vm.ReceiptNo = tb.ReceiptNo;
                vm.Date = tb.Date;
                vm.Description = tb.Description;
                vm.ReceivedAmount = tb.ReceivedAmount;
                vm.UnusedAmount = tb.UnusedAmount;
                vm.Total = tb.Total;
                vm.Status = tb.Status;
                //vm.image = tb.image;
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Paymentin( tbl_PaymentInSelectResult pay, int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Insert1",null, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    //CustomerName as PartyName,PaymentType,ReceiptNo,Date,Description,ReceivedAmount, UnusedAmount,Total,Status,image
                    db.tbl_PaymentInSelect("Update1", id, pay.PartyName, pay.PaymentType, pay.ReceiptNo, Convert.ToDateTime(pay.Date), pay.Description, pay.ReceivedAmount, pay.UnusedAmount, pay.image, pay.Total, pay.Status, null, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_PaymentInSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }
    }
}