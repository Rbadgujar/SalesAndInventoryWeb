using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.ViewModel;

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
        public ActionResult registration(string number)
        {
            Session["number"] = number;
            return View();
        }
        public static int companyid;
        byte[] bytes;
        string pataa;
        [HttpPost]
        public ActionResult registration(HttpPostedFileBase file,tbl_CompanyMasterSelectResult com)
        {
           

            try
            {
                
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                  var pata = Path.Combine(Server.MapPath("~/images/Logo"), fileName);
                    file.SaveAs(pata);
                    pataa = "~/images/Logo/" + fileName;
                }
                //using (BinaryReader br = new BinaryReader(file.InputStream))
                //{
                //    bytes = br.ReadBytes(file.ContentLength);
                //}

            }
            catch (Exception ew)
            {

            }
            db.tbl_CompanyMasterSelect("Insert2", null, com.CompanyName,Session["number"].ToString(), com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature,null, com.BankName, com.AccountNo, com.IFSC_Code, com.CompanyID,pataa, null);
            db.SubmitChanges();         
            return RedirectToAction("Index", "MainLogin");        
        }
        [HttpPost]
 
        public ActionResult registrationinserty(Companymaster com)
        {
            db.tbl_CompanyMasterSelect("Insert2", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.banlname, com.Accountno, com.IFScCode, com.CompanyID, null,com.Password);
            db.SubmitChanges();

            return RedirectToAction("Index", "MainLogin");
        }
    }


}