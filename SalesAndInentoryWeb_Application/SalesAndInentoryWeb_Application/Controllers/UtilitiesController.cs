using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using ClosedXML.Excel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using OfficeOpenXml;
using System.Text;
using SalesAndInentoryWeb_Application.ViewModel;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class UtilitiesController : Controller
    {
      
        // GET: Utilities
      
       
        public ActionResult Importitem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Importitem1(HttpPostedFileBase file)
        {
            try { 
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension =System.IO.Path.GetExtension(Request.Files["file"].FileName);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.  
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.  
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace  
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.  
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                        System.Xml.XmlTextReader xmlreader = new System.Xml.XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();  
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string conn = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(conn);
                    //string query = "insert into tbl_ItemMaster(ItemName,HSNCode) Values('" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "','" + ds.Tables[0].Rows[i]["HSNCode"].ToString() + "')";

                   string query = "insert into tbl_ItemMaster(ItemName,HSNCode ,BasicUnit,SecondaryUnit,ItemCode ,ItemCategory,SalePrice,TaxForSale ,SaleTaxAmount,PurchasePrice,TaxForPurchase,PurchaseTaxAmount ,atPrice,OpeningQty ,Date,ItemLocation,TrackingMRP,BatchNo,Company_ID) Values('" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "','" + ds.Tables[0].Rows[i]["HSNCode"].ToString() + "','" + ds.Tables[0].Rows[i]["BaseUnit"].ToString() + "','" + ds.Tables[0].Rows[i]["SecondaryUnit"].ToString() + "','" + ds.Tables[0].Rows[i]["ItemCode"].ToString() + "','" + ds.Tables[0].Rows[i]["ItemCategory"].ToString() + "','" + ds.Tables[0].Rows[i]["SalePrice"].ToString() + "','" + ds.Tables[0].Rows[i]["TaxForSale"].ToString() + "','" + ds.Tables[0].Rows[i]["SaleTaxAmount"].ToString() + "','" + ds.Tables[0].Rows[i]["PurchasePrice"].ToString() + "','" + ds.Tables[0].Rows[i]["TaxForPurchase"].ToString() + "','" + ds.Tables[0].Rows[i]["PurchaseTaxAmount"].ToString() + "','" + ds.Tables[0].Rows[i]["OpeningQty"].ToString() + "','" + ds.Tables[0].Rows[i]["atPrice"].ToString() + "','" + Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString() )+ "','" + ds.Tables[0].Rows[i]["ItemLocation"].ToString() + "','" + ds.Tables[0].Rows[i]["TrackingMRP"].ToString() + "','" + ds.Tables[0].Rows[i]["BatchNo"].ToString() + "',"+MainLoginController.companyid1+")";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                    ViewBag.messeage = "Successfully Stored ";
                    return View("importitem");
                    //return Json(new { result = false, msg = "Suucessfully Stored" });
            }

                ViewBag.messeage = "Select File ";
                return View("importitem");
            }
            catch(Exception ew)
            {
                ViewBag.messeage = "Invalid Information ";
                return View("importitem");
            }

        }
        public FileResult DownloadFile(string Name)
        {

            var Name1 = "ItemImport.xlsx";
            string path = Server.MapPath("~/exportfile/") + Name1;

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", Name1);

        }

        [HttpPost]
        public ActionResult ImportParty(HttpPostedFileBase file)
        {
          
                DataSet ds = new DataSet();
                if (Request.Files["file"].ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["file"].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.  
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.  
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace  
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.  
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();  
                        ds.ReadXml(xmlreader);
                        xmlreader.Close();
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string conn = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(conn);
                        //string query = "insert into tbl_ItemMaster(ItemName,HSNCode) Values('" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "','" + ds.Tables[0].Rows[i]["HSNCode"].ToString() + "')";
                        string query = "insert into tbl_PartyMaster (PartyName,ContactNo,BillingAddress,EmailID,GSTType,State,OpeningBal,AsOfDate,AddRemainder,PartyType,ShippingAddress,PartyGroup,PaidStatus,Company_ID) Values('" + ds.Tables[0].Rows[i]["PartyName"].ToString() + "','" + ds.Tables[0].Rows[i]["ContactNo"].ToString() + "','" + ds.Tables[0].Rows[i]["BillingAddress"].ToString() + "','" + ds.Tables[0].Rows[i]["EmailID"].ToString() + "','" + ds.Tables[0].Rows[i]["GSTType"].ToString() + "','" + ds.Tables[0].Rows[i]["State"].ToString() + "','" + ds.Tables[0].Rows[i]["OpeningBal"].ToString() + "','" +ds.Tables[0].Rows[i]["AsOfDate"].ToString() + "','" + ds.Tables[0].Rows[i]["AddRemainder"].ToString() + "','" + ds.Tables[0].Rows[i]["PartyType"].ToString() + "','" + ds.Tables[0].Rows[i]["ShippingAddress"].ToString() + "','" + ds.Tables[0].Rows[i]["PartyGroup"].ToString() + "','" + ds.Tables[0].Rows[i]["PaidStatus"].ToString() + "',"+MainLoginController.companyid1+")";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                ViewBag.messeage = "Successfully Stored ";
                //return Json(new { result = false, msg = "Suucessfully Stored" });
                return View("ImportParty");

            }
            ViewBag.messeage = "Select File";
            //return Json(new { result = false, msg = "Select file" });     
            return View("ImportParty");
        }

        public FileResult DownloadFile1(string Name)
        {

            var Name1 = "Import Parties Template (1).xlsx";
            string path = Server.MapPath("~/exportfile/") + Name1;

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", Name1);

        }
        [HttpGet]
        public ActionResult ImportParty()
        {
            return View();
        }
       
        // GET: Utilities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Utilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilities/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Utilities/Edit/5
        public ActionResult Export()
        {
            return View();
        }

        public void ExportListUsingEPPlus()
        {
    
          
            List<exportitem> data = new List<exportitem>();
            string CS = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_ItemMaster where Company_ID="+MainLoginController.companyid1+" and DeleteData='1'", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var list = new exportitem();
                    //list. = Convert.ToInt32(rdr["id"]);
                  
                    list.ItemName = rdr["ItemName"].ToString();
                    list.HSNCode = rdr["HSNCode"].ToString();
                    list.ItemLocation = rdr["ItemLocation"].ToString();
                    list.BasicUnit= rdr["BasicUnit"].ToString();

                    list.SecondaryUnit = rdr["SecondaryUnit"].ToString();
                    list.ItemCode = rdr["ItemCode"].ToString();
                    list.ItemCategory = rdr["ItemCategory"].ToString();
                    list.SalePrice = rdr["SalePrice"].ToString();
                    list.TaxForSale = rdr["TaxForSale"].ToString();
                    list.SaleTaxAmount =rdr["SaleTaxAmount"].ToString();
                    list.Date = rdr["Date"].ToString();
                    list.PurchasePrice = rdr["PurchasePrice"].ToString();
                    list.TaxForPurchase = rdr["TaxForPurchase"].ToString();
                    list.PurchaseTaxAmount =rdr["PurchaseTaxAmount"].ToString();
                    list.OpeningQty = rdr["OpeningQty"].ToString();
                    list.atPrice = rdr["atPrice"].ToString();
                    list.ItemLocation = rdr["ItemLocation"].ToString();



                    list.TrackingMRP = rdr["TrackingMRP"].ToString();

                    list.BatchNo = rdr["BatchNo"].ToString();
                    list.MFgdate = rdr["MFgdate"].ToString();
                    list.Expdate = rdr["Expdate"].ToString();
                    list.Description = rdr["Description"].ToString();
                    list.MinimumStock =rdr["MinimumStock"].ToString();
                    list.Barcode = rdr["Barcode"].ToString();
                    list.Image1 = rdr["Image1"].ToString();
                    data.Add(list);
                }
               
            }




            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            workSheet.Column(1).Width = 50;
            workSheet.Column(2).Width = 20;
            workSheet.Column(3).Width =20;
            workSheet.Column(4).Width = 20;
            workSheet.Column(5).Width = 20;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;
            workSheet.Column(8).Width = 20;
            workSheet.Column(9).Width = 20;
            workSheet.Column(10).Width = 20;
            workSheet.Column(11).Width = 20;
            workSheet.Column(12).Width = 20;
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //here i have set filname as Students.xlsx
                Response.AddHeader("content-disposition", "attachment;  filename=All Item Details.xlsx");
             
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void saleinvoicedata()
        {


            List<Exportsaledata> data = new List<Exportsaledata>();
            string CS = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_SaleInvoice where Company_ID=" + MainLoginController.companyid1 + " and Deletedata='1'", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var list = new Exportsaledata();
                    //list. = Convert.ToInt32(rdr["id"]);
                    list.InvoiceID = rdr["InvoiceID"].ToString();
                    list.PartyName = rdr["PartyName"].ToString();
                    list.BillingName = rdr["BillingName"].ToString();
                    list.ContactNo = rdr["ContactNo"].ToString();
                    list.PONumber = rdr["PoNumber"].ToString();
                    list.InvoiceDate =rdr["InvoiceDate"].ToString();
                    list.StateOfSupply = rdr["StateofSupply"].ToString();

                    list.PaymentType = rdr["PaymentType"].ToString();
                    list.TransportName = rdr["TransportName"].ToString();
                    list.CGST = rdr["CGST"].ToString();
                    list.SGST = rdr["SGST"].ToString();
                    list.IGST = rdr["IGST"].ToString();

                    
                    //list.TaxAmount1 = rdr["TaxAmount1"].ToString();
                    list.TotalDiscount = rdr["TotalDiscount"].ToString();
                    list.Received =rdr["Received"].ToString();
                    list.RemainingBal = rdr["RemainingBal"].ToString(); 
                    list.Total = rdr["Total"].ToString();
                    data.Add(list);
                }

            }




            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            workSheet.Column(1).Width = 50;
            workSheet.Column(2).Width = 20;
            workSheet.Column(3).Width = 20;
            workSheet.Column(4).Width = 20;
            workSheet.Column(5).Width = 20;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //here i have set filname as Students.xlsx
                Response.AddHeader("content-disposition", "attachment;  filename=All Parties Data.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


        public void purchasebilldata()
        {


            List<Exportpurchase> data = new List<Exportpurchase>();
            string CS = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_PurchaseBill where Company_ID=" + MainLoginController.companyid1 + " and Deletedata='1'", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var list = new Exportpurchase();
                    //list. = Convert.ToInt32(rdr["id"]);s
                    list.BillNo = rdr["BillNo"].ToString();
                    list.PartyName = rdr["PartyName"].ToString();
                    list.BillingName = rdr["BillingName"].ToString();
                    list.ContactNo = rdr["ContactNo"].ToString();
                    list.PONumber = rdr["PONo"].ToString();
                    list.BillDate = rdr["BillDate"].ToString();
                    list.StateOfSupply = rdr["StateofSupply"].ToString();

                    list.PaymentType = rdr["PaymentType"].ToString();
                    list.TransportName = rdr["TransportName"].ToString();
                    list.CGST = rdr["CGST"].ToString();
                    list.SGST = rdr["SGST"].ToString();
                    list.IGST = rdr["IGST"].ToString();


                    //list.TaxAmount1 = rdr["TaxAmount1"].ToString();
                    list.TotalDiscount = rdr["TotalDiscount"].ToString();
                    //list.Received = rdr["Received"].ToString();
                  list.RemainingBal = rdr["RemainingBal"].ToString();
                    list.Total = rdr["Total"].ToString();
                    data.Add(list);
                }

            }
            



            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            workSheet.Column(1).Width = 50;
            workSheet.Column(2).Width = 20;
            workSheet.Column(3).Width = 20;
            workSheet.Column(4).Width = 20;
            workSheet.Column(5).Width = 20;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //here i have set filname as Students.xlsx
                Response.AddHeader("content-disposition", "attachment;  filename=All Parties Data.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }




        public void exportpartyes()
        {


            List<Exportpartyes> data = new List<Exportpartyes>();
            string CS = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_PartyMaster where Company_ID="+MainLoginController.companyid1+"", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var list = new Exportpartyes();
                    //list. = Convert.ToInt32(rdr["id"]);

                    list.PartyName = rdr["PartyName"].ToString();
                    list.ContactNo = rdr["ContactNo"].ToString();
                    list.BillingAddress = rdr["BillingAddress"].ToString();
                    list.EmailID = rdr["EmailID"].ToString();
                    list.GST_NO = rdr["GSTType"].ToString();
                    list.State = rdr["State"].ToString();
                    
                    list.OpeningBal = rdr["OpeningBal"].ToString();
                    list.AsOfDate = rdr["AsOfDate"].ToString();
                    list.AddRemainder = rdr["AddRemainder"].ToString();
                    list.PartyType = rdr["PartyType"].ToString();


                    list.ShippingAddress = rdr["ShippingAddress"].ToString();
                    list.PartyGroup = rdr["PartyGroup"].ToString();
                  
                    data.Add(list);
                }

            }




            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            workSheet.Column(1).Width = 50;
            workSheet.Column(2).Width = 20;
            workSheet.Column(3).Width = 20;
            workSheet.Column(4).Width = 20;
            workSheet.Column(5).Width = 20;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;
      
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //here i have set filname as Students.xlsx
                Response.AddHeader("content-disposition", "attachment;  filename=All Parties Data.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


        // POST: Utilities/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Utilities/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Utilities/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
