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
            //using (idealtec_inventoryEntities10 entities = new idealtec_inventoryEntities10())
            //{
            //    List<tbl_PartyMaster> party = entities.PartyMasterCrud("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();

            //    //Add a Dummy Row.
            //    party.Insert(0, new tbl_PartyMaster());
            //    return View(party.ToList());
            //}
            return View();
        }

        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PartyMasterSelect("Select1", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
       public ActionResult groupdata()
        {
            var getdata = db.tbl_PartyGroupSelect("Select",null,null,null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartyGroupData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<tbl_PartyGroup> party = db.tbl_PartyGroup.ToList<tbl_PartyGroup>();
                return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddOrEdit()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddOrEdit(int id,tbl_PartyMasterSelectResult party)//int id = 0)
        {
            try
            {
                //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID,null, party.State, party.OpeningBal, null, party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup,null, party.PaidStatus,null);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
        public ActionResult PartyGroup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult makykdata()
        {

            var getdata = db.tbl_PartyGroupSelect("Select1", null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }


    }

}