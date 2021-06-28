using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SalesAndInentoryWeb_Application.Models;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CompanyController : Controller
    {
        DataClassesCompanyDataContext dc = new DataClassesCompanyDataContext();

        // GET: Company
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult com()
        //{
        //    return View();

        //}

        public ActionResult ShowData()
        {
            var getdata = dc.tbl_CompanyMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return View(getdata);
            //return Json(new { data=getdata},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index() //Insert Operation
        {
            return View();
        }

        [HttpPost]
        public ActionResult Company(tbl_CompanyMaster com)
        {
            try
            {
                //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                dc.tbl_CompanyMasterSelect("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.RefferalCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null);
                dc.SubmitChanges();
                return RedirectToAction("ShowData");
            }
            catch
            {
                return View();
            }
        }


        //[HttpPost]
        //public ActionResult Company(tbl_CompanyMaster com)
        //{
        //    using (idealtec_inventoryEntities10 entities = new idealtec_inventoryEntities10())
        //    {
        //        tbl_CompanyMaster comp = entities.PerformCRUD_Comp("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3,null).FirstOrDefault();
        //        return View(comp);
        //    }
        //    //return View();
        //}
    }
}