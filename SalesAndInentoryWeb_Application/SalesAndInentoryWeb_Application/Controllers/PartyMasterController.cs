
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using Path = System.IO.Path;
using System.Configuration;
using System.Data.SqlClient;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PartyMasterController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: PartyMaster
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Addparty(int id=0)
        {
            if (id == 0)
            {
                tbl_PartyMaster bt = new tbl_PartyMaster();
                bt.ListOfPartyGroup = ListOfItems();
                return View(bt);
            }
            else
            {
                var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null).Single(x => x.PartiesID == id);
                var vm = new tbl_PartyMasterSelectResult();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.PartyName = tb.PartyName;
                vm.ContactNo = tb.ContactNo;
                vm.BillingAddress = tb.BillingAddress;
                vm.EmailID = tb.EmailID;
              //vm.GSTNo = tb.GSTNo;         
                vm.State = tb.State;
                vm.OpeningBal = tb.OpeningBal;
                vm.AsOfDate = Convert.ToDateTime(tb.AsOfDate);
                vm.AddRemainder = tb.AddRemainder;
                vm.PartyType = tb.PartyType;
                vm.ShippingAddress = tb.ShippingAddress;
                vm.PartyGroup = tb.PartyGroup;
                vm.PaidStatus = tb.PaidStatus;
                // vm.Type = tb.Type;
                return View(vm);
            }
        }
        public ActionResult dublication(string id)
        {
            var getdata = db.tbl_PartyMasterSelect("dublication", null, id, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null).ToList();

            //var getdata = db.tbl_ItemMasterSelect("dublication", null, id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"].ToString()), null, null, null, null).ToList();
             return Json(new { data = getdata.Count }, JsonRequestBehavior.AllowGet);

        }
        public string pataa = "";
        [HttpPost]
        public ActionResult Addparty(HttpPostedFileBase file, tbl_PartyMaster party, int id = 0)
        {
            if (id == 0)
             {
                if(party.OpeningBal==null)
                {
                    party.OpeningBal = 0;
                }
                try
                {
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/images/Partyimage"), fileName);
                            file.SaveAs(path);
                            pataa = "~/images/Partyimage" + fileName;
                        }
                    }
                    catch (Exception ew)
                    {

                    }
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_PartyMasterSelect("Insert1", null, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTType, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, Convert.ToInt32(Session["UserId"]), party.PaidStatus, pataa);
                    db.SubmitChanges();
                    return RedirectToAction("index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    //return View(ex);
                }
                return View();
            }
            else
            {
                db.tbl_PartyMasterSelect("Update", id, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.GSTType, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, Convert.ToInt32(Session["UserId"]), party.PaidStatus, null);
                db.SubmitChanges();
                return RedirectToAction("Index");
                //return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ShowData()
        {
            var getdata = db.tbl_PartyMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null).Single(x => x.PartiesID == id);
            return View(tb);
        }
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT AddPartyGroup FROM tbl_PartyGroup where DeleteData='1' and Company_ID='" + MainLoginController.companyid1 + "'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["AddPartyGroup"].ToString(),
                                Value = sdr["AddPartyGroup"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        [HttpGet]
        public ActionResult AddpartyUpdate(int id = 0)
        {
            if (id == 0)
            {
                return View(new tbl_PartyMaster());
            }
            else
            {
                var tb = db.tbl_PartyMasterSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null,Convert.ToInt32(Session["UserId"]), null, null).Single(x => x.PartiesID == id);
                var vm = new tbl_PartyMaster();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.PartyName = tb.PartyName;
                vm.ContactNo = tb.ContactNo;
                vm.BillingAddress = tb.BillingAddress;
                vm.EmailID = tb.EmailID;
                vm.GSTType = tb.GSTNo;
                vm.State = tb.State;
                vm.OpeningBal = tb.OpeningBal;
                vm.AsOfDate = Convert.ToDateTime(tb.AsOfDate);
                vm.AddRemainder = tb.AddRemainder;
                vm.PartyType = tb.PartyType;
                vm.type = tb.Type;          
                vm.ShippingAddress = tb.ShippingAddress;
                vm.PartyGroup = tb.PartyGroup;
                vm.PaidStatus = tb.PaidStatus;
               // vm.Type = tb.Type;
                return View(vm);
            }
        }
        
        [HttpPost]
        public ActionResult AddpartyUpdate(HttpPostedFileBase file,tbl_PartyMaster party, int id=0)//int id = 0)
        {
            if (party.OpeningBal == null)
            {
                party.OpeningBal = 0;
            }
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/Partyimage"),fileName);
                    file.SaveAs(path);
                    pataa = "~/images/Partyimage" + fileName;
                }
            }
            catch (Exception ew)
            {

            }
            db.tbl_PartyMasterSelect("Update1", id, party.PartyName, party.ContactNo, party.BillingAddress, party.EmailID, party.PartyGroup, party.State, party.OpeningBal, Convert.ToDateTime(party.AsOfDate), party.AddRemainder, party.PartyType, party.ShippingAddress, party.PartyGroup, Convert.ToInt32(Session["UserId"]), party.PaidStatus,pataa);
           db.SubmitChanges();
           return RedirectToAction("Index");
          
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var getdata = db.tbl_PartyMasterSelect("Delete", id,null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"]), null, null).ToList();
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
        [HttpGet]
        public ActionResult PartyGroup(int id=0)
        {
            if (id == 0)
            {
                return View(new tbl_PartyGroup());
            }
            else
            {
                var tb = db.tbl_PartyGroupSelect("Details", id, null, Convert.ToInt32(Session["UserId"])).Single(x => x.PartyGroupID == id);
                var vm = new tbl_PartyGroup();
                //PartiesID,PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate
                //AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Type
                vm.AddPartyGroup = tb.AddPartyGroup;
              
                // vm.Type = tb.Type;
                return View(vm);
            }

        }

        [HttpPost]
        public ActionResult PartyGroup(tbl_PartyGroup party, int id = 0)
        {

            if (id == 0)
            {
                try
                {
                    //("Insert", null, com.CompanyName, com.PhoneNo, com.EmailID, com.ReferaleCode, com.BusinessType, com.Address, com.City, com.State, com.GSTNumber, com.OwnerName, com.Signature, com.AddLogo, com.AdditinalFeild1, com.AdditinalFeild2, com.AdditinalFeild3, null).FirstOrDefault();
                    db.tbl_PartyGroupSelect("Insert",null,party.AddPartyGroup, MainLoginController.companyid1);
                    db.SubmitChanges();
                    return RedirectToAction("index");
                    //return Json(new { success = true, message = "Saved Data Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                db.tbl_PartyGroupSelect("Update", id,party.AddPartyGroup, MainLoginController.companyid1);
                db.SubmitChanges();
                return RedirectToAction("index");
                //return Json(new { success = true, message = "Update Data Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult Partygroupindex()
        {
            return View();
        }
        public ActionResult PartyGroupData()
        {
            //using (idealtec_inventoryEntities10 db = new idealtec_inventoryEntities10())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;
            //    List<tbl_PartyGroup> party = db.tbl_PartyGroup.ToList<tbl_PartyGroup>();
            //    return Json(new { data = party }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        public ActionResult DetailPartyGroup(int id)
        {
            var tb = db.tbl_PartyGroupSelect("Details", id, null, null).Single(x => x.PartyGroupID == id);
            return View(tb);
        }

        [HttpGet]
        public ActionResult partygroupshow()
        {
            var getdata = db.tbl_PartyGroupSelect("Select", null, null, MainLoginController.companyid1).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult deletepartygroup(int id)
        {
            try
            {
                var getdata = db.tbl_PartyGroupSelect("Delete", id, null, MainLoginController.companyid1).ToList();
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
    }

}