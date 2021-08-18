﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Stimulsoft.Report.Mvc;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ItemTrackingReportController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

         [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Data()
        {
            var tb = db.ItemTrackingReport("ItemTrackingReport", null, null, null, null,null,null, Convert.ToInt32(Session["UserId"])).ToList();
            return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.Size,b.MFgDate,b.ExpDate,b.ItemName,b.BatchNo,b.SerialNo,b.Company_ID from tbl_ItemMaster as b,tbl_CompanyMaster as c where b.Company_ID = '" + MainLoginController.companyid1 + "' and c.CompanyID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "ItemTrackingData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/ItemTrackingData.mrt"));
            report.RegData("ItemTrackingData", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
    }
}