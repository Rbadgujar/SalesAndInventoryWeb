﻿using DevExpress.Web.Mvc;
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
            using (idealtec_inventoryEntities10 entities = new idealtec_inventoryEntities10())
            {
                List<tbl_PartyMaster> party = entities.PartyMasterCrud("Select", null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null).ToList();

                //Add a Dummy Row.
                party.Insert(0, new tbl_PartyMaster());
                return View(party.ToList());
            }
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
        [HttpPost]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new tbl_PartyMaster());
            else
            {
                using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
                {
                    return View(db.tbl_PartyMaster.Where(x => x.PartiesID == id).FirstOrDefault<tbl_PartyMaster>());
                }
            }
        }
        public ActionResult PartyGroup()
        {
            return View();
        }
    }

}