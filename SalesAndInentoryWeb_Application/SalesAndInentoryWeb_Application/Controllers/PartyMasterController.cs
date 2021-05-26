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
        public ActionResult PartyData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_PartyMaster> bank = db.tbl_PartyMaster.ToList<tbl_PartyMaster>();
                return Json(new { data = bank }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PartyGroupData()
        {
            using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            {
                List<tbl_PartyGroup> party = db.tbl_PartyGroup.ToList<tbl_PartyGroup>();
                return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
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