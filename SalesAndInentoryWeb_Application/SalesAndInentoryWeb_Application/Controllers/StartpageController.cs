using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class StartpageController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Startpage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult registration()
        {
            return View();
        }
        public static int companyid;
        [HttpPost]
        public ActionResult registration(tbl_CompanyMasterSelectResult com)
        {

            //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();

            db.tbl_CompanyMasterSelect("Insert1", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, com.CompanyID);
            db.SubmitChanges();
            //var tb = db.tbl_CompanyMasterSelect("max", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single();
            //companyid = tb.CompanyID;

               //string idd = com.EmailID;
               //string pass = com.ReferaleCode;
               //db.tbl_LoginPassswordSelect("Insert",null,pass,null,ff,idd);
               //db.SubmitChanges();

                return RedirectToAction("Index", "MainLogin");        
        }
    }


}