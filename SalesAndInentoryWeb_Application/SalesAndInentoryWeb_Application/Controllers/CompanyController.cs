using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.Models;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult com()
        {
            return View();
                 
        }
        [HttpGet]
        public ActionResult showdata()
        {
          
            var getdata = db.tbl_CompanyMasterSelect("Select",null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Delete(int id, tbl_CompanyMasterSelectResult com)
        {
            try
            {
                var getdata = db.tbl_CompanyMasterSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
                // return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Company()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Company(int id,tbl_CompanyMasterSelectResult com)
        {
            if (id != null)
            {
                try
                {

                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_CompanyMasterSelect("Update", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
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

                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_CompanyMasterSelect("Insert", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, null);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }

        

    }
}