using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application;
using SalesAndInentoryWeb_Application.Models;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Path = System.IO.Path;

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
        [HttpGet]
        public ActionResult com(int id=0)
        {

            if (id == 0)
            {
                return View();
            }
            else
            {
                var tb = db.tbl_CompanyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).Single(x => x.CompanyID == id);
                var vm = new tbl_CompanyMaster();
                vm.CompanyName = tb.CompanyName;
                vm.ReferaleCode = tb.ReferaleCode;
                vm.Address = tb.Address;
                vm.EmailID = tb.EmailID;
                vm.City = tb.City;
               // vm.AddLogo = tb.AddLogo;
                vm.PhoneNo = tb.ContactNo;
                vm.GSTNumber = tb.GSTNumber;
                vm.BusinessType = tb.BusinessType;
                vm.State = tb.State;
             
                return View(vm);
            }

          
        }
        [HttpPost]

        public ActionResult com( tbl_CompanyMasterSelectResult com, int id=0)
        {


            try
            {
                if (id == 0)
                {
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_CompanyMasterSelect("Insert", null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, com.CompanyID);
                    
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.tbl_CompanyMasterSelect("Update", id, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code, com.CompanyID);
                    db.SubmitChanges();
                        return RedirectToAction("Index");
                        //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);                   
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public static List<CountryList> GetCountriesName()
        {
            List<CountryList> lst = new List<CountryList>();
            lst.Add(new CountryList() { CountryId = 1, CountryName = "India" });
            lst.Add(new CountryList() { CountryId = 2, CountryName = "Nepal" });
            lst.Add(new CountryList() { CountryId = 3, CountryName = "America" });
            return lst;

        }
        [HttpGet]
        public ActionResult showdata()
        {
          
            var getdata = db.tbl_CompanyMasterSelect("Select",null,null,null,null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_CompanyMasterSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
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
        public ActionResult Company()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Company(HttpPostedFileBase file, tbl_CompanyMasterSelectResult com)
        {

            try
            {
                //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                db.tbl_CompanyMasterSelect("Insert",null, com.CompanyName, com.ContactNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.BankName, com.AccountNo, com.IFSC_Code,com.CompanyID);
                if (file!=null  && file.ContentLength>0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string fileext = Path.GetExtension(filename);
                    if(fileext == ".jpg" || fileext ==".png")
                    {
                        string filepath = Path.Combine(Server.MapPath("~/images/Logo"),filename);
                        string mainconn = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                        SqlConnection sqlcon = new SqlConnection(mainconn);
                        SqlCommand sqlcmd = new SqlCommand("Insert",sqlcon);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcon.Open();
                        sqlcmd.Parameters.AddWithValue("@AddLogo", filename);
                        sqlcmd.Parameters.AddWithValue("@signature", filename);
                        sqlcmd.ExecuteNonQuery();
                        file.SaveAs(filepath);
                        sqlcon.Close();
                    }
                }
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }
 
    }
}