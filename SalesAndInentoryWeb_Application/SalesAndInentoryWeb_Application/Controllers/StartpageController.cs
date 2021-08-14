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
            db.tbl_CompanyMasterSelect("Insert1", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, com.CompanyID);
            db.SubmitChanges();         
            return RedirectToAction("Index", "MainLogin");        
        }
    }


}