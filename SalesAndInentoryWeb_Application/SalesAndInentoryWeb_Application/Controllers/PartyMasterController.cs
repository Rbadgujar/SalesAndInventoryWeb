using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PartyMasterController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: PartyMaster
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Addparty(int id=0)
        {
            if (id == 0)
            {
                return View(new tbl_PartyMaster());
            }
            else
            {
                var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.PartiesID == id);
                var vm = new tbl_PartyMaster();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.PartyName = tb.PartyName;
                vm.ContactNo = tb.ContactNo;
                vm.BillingAddress = tb.BillingAddress;
                vm.EmailID = tb.EmailID;
              //vm.GSTNo = tb.GSTNo;
          
                vm.State = tb.State;
                vm.OpeningBal = tb.OpeningBal;
                vm.AsOfDate = Convert.ToDateTime(tb.AsOfDate);
                vm.AddRemainder = tb.AddRemainder;
                vm.PartyType = tb.PartyType;
                vm.ShippingAddress = tb.ShippingAddress;
                vm.PartyGroup = tb.PartyGroup;
                vm.PaidStatus = tb.PaidStatus;
                // vm.Type = tb.Type;
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Addparty(tbl_PartyMasterSelectResult party, int id = 0)
        {
            if (id == 0)
        {
                try
                {
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTNo, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, null, party.PaidStatus, null);
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
                db.tbl_PartyMasterSelect("Update", id, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTNo, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, null, party.PaidStatus, null);
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PartyMasterSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.PartiesID == id);
            return View(tb);
        }
        
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new tbl_PartyMaster());
            }
            else
            {
                var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.PartiesID == id);
                var vm = new tbl_PartyMaster();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.PartyName = tb.PartyName;
                vm.ContactNo = tb.ContactNo;
                vm.BillingAddress = tb.BillingAddress;
                vm.EmailID = tb.EmailID;
               // vm.GSTNo = tb.GSTNo;
                vm.State = tb.State;
                vm.OpeningBal = tb.OpeningBal;
                vm.AsOfDate = Convert.ToDateTime(tb.AsOfDate);
                vm.AddRemainder = tb.AddRemainder;
                vm.PartyType = tb.PartyType;
                vm.ShippingAddress = tb.ShippingAddress;
                vm.PartyGroup = tb.PartyGroup;
                vm.PaidStatus = tb.PaidStatus;
               // vm.Type = tb.Type;
                return View(vm);
            }
        }
        
        [HttpPost]
        public ActionResult AddOrEdit(tbl_PartyMasterSelectResult party, int id=0)//int id = 0)
        {
            if (id == 0)
            {
                try
                {
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTNo, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, null, party.PaidStatus, null);
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
                db.tbl_PartyMasterSelect("Update", id, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTNo, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, null, party.PaidStatus, null);
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_PartyMasterSelect("Delete", id,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult PartyGroup(int id=0)
        {
            if (id == 0)
            {
                return View(new tbl_PartyGroup());
            }
            else
            {
                var tb = db.tbl_PartyGroupSelect("Details", id, null, null).Single(x => x.PartyGroupID == id);
                var vm = new tbl_PartyGroup();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.AddPartyGroup = tb.AddPartyGroup;
              
                // vm.Type = tb.Type;
                return View(vm);
            }

        }

        [HttpPost]
        public ActionResult PartyGroup(tbl_PartyGroup party, int id = 0)
        {

            if (id == 0)
            {
                try
                {
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_PartyGroupSelect("Insert1",null,party.AddPartyGroup,null);
                    db.SubmitChanges();
                    return RedirectToAction("index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                db.tbl_PartyGroupSelect("Update", id,party.AddPartyGroup, null);
                db.SubmitChanges();
                return RedirectToAction("index");
                //return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult Partygroupindex()
        {
            return View();
        }
        public ActionResult PartyGroupData()
        {
            //using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;
            //    List<tbl_PartyGroup> party = db.tbl_PartyGroup.ToList<tbl_PartyGroup>();
            //    return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        public ActionResult DetailPartyGroup(int id)
        {
            var tb = db.tbl_PartyGroupSelect("Details", id, null, null).Single(x => x.PartyGroupID == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult partygroupshow()
        {
            var getdata = db.tbl_PartyGroupSelect("Select1", null, null,null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult deletepartygroup(int id)
        {
            try
            {
                var getdata = db.tbl_PartyGroupSelect("Delete", id, null, null).ToList();
                db.SubmitChanges();
                return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}