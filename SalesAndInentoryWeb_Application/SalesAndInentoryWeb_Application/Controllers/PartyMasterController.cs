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
        public ActionResult Data()
        {
            //using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;
            //    List<tbl_PartyMaster> party = db.tbl_PartyMaster.ToList<tbl_PartyMaster>();
            //    return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            //}
            //using (idealtec_inventoryEntities10 entities = new idealtec_inventoryEntities10())
            //{
            //    List<tbl_PartyMaster> party = entities.PartyMasterCrud("Select", null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null).ToList();

            //    //Add a Dummy Row.
            //    party.Insert(0, new tbl_PartyMaster());
            //    return View(party.ToList());
            //}
            return View();
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
        [HttpGet]
        public ActionResult AddOrEdit()//int id = 0)
        {

            return View();
           
        }

        [HttpPost]
        //public ActionResult AddOrEdit(tbl_PartyMaster party)//int id = 0)
        //{

        //    //using (idealtec_inventoryEntities10 entities = new idealtec_inventoryEntities10())
        //    //{
        //    //    tbl_PartyMaster party1 = entities.tbl_PartyMaster("Insert", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTType, party.State, party.OpeningBal, party.AsOfDate, party.AddRemainder,  party.PartyType, party.ShippingAddress,  party.PartyGroup,null,party.PaidStatus, null ).FirstOrDefault();
        //    //    return View(party1);
        //    //}

        //    //if (id == 0)
        //    //    return View(new tbl_PartyMaster());
        //    //else
        //    //{
        //    //    using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
        //    //    {
        //    //        return View(db.tbl_PartyMaster.Where(x => x.PartiesID == id).FirstOrDefault<tbl_PartyMaster>());
        //    //    }
        //    //}
        //}
        public ActionResult PartyGroup()
        {
            return View();
        }
    }

}