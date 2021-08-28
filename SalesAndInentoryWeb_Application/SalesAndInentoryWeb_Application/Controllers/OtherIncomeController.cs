using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Mvc;
using SalesAndInentoryWeb_Application.ViewModel;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class OtherIncomeController : Controller
    {

        CompanyDataClassDataContext db = new CompanyDataClassDataContext();

        // GET: OtherIncome
        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public ActionResult Data(string date, string date2, string par)
        {
            if (par == "0")
            {
                var tb1 = db.tbl_OtherIncomeSelect("datetodate", null, null, Convert.ToDateTime(date), null, null, null, null, null, null, null, date2, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
                return Json(new { data = tb1 }, JsonRequestBehavior.AllowGet);
            }
            var tb = db.tbl_OtherIncomeSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Detail(int id)
		{
			var tb = db.tbl_OtherIncomeSelect("Details", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).Single(x => x.Id == id);
			return View(tb);
		}
        private static List<SelectListItem> ListOfAccount()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster where DeleteData='1' and Company_ID='"+MainLoginController.companyid1+"'");
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
                                Text = sdr["ItemName"].ToString(),
                                Value = sdr["ItemName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        public JsonResult GetFruitName(string id)
        {
            return Json(GetFruitNameById(id), JsonRequestBehavior.AllowGet);
        }
        private static string GetFruitNameById(string id)
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT SalePrice FROM tbl_ItemMaster WHERE ItemName = @Id");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();

                    string name = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();

                    return name;
                }
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
			
                tbl_OtherIncome bt = new tbl_OtherIncome();
                bt.ListOfAccounts = ListOfAccount();
                bt.ListOfCategory = ListOfCategorys();
                return View(bt);
			
        }

        

        private static List<SelectListItem> ListOfCategorys()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_otherIncomeCaategory where DeleteData='1'  and Company_ID='" + MainLoginController.companyid1 + "'");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items1.Add(new SelectListItem
                            {
                                Text = sdr["OtherIncome"].ToString(),
                                Value = sdr["OtherIncome"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
        }

        int maxdata;

        public int maxcount()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
              string  sql = string.Format("Select Max(id1) from tbl_OtherIncome");
                SqlCommand cmd = new SqlCommand(sql,con);
                con.Open();
                maxdata =Convert.ToInt32(cmd.ExecuteScalar());
               
            }
            return maxdata;

        }
        [HttpPost]
        public ActionResult AddOrEdit(OtherIncome objOtherIncomeDetails)
        {
            maxdata = maxcount();
            int dinmax = maxdata + 1;
            tbl_OtherIncome sale1other = new tbl_OtherIncome()
            {
                IncomeCategory = objOtherIncomeDetails.IncomeCategory,
                total = objOtherIncomeDetails.total,
                Balance = objOtherIncomeDetails.Balance,
                Id1 = dinmax,
                Date = objOtherIncomeDetails.Date,
                Received = objOtherIncomeDetails.Received,
                
                Company_ID= Convert.ToInt32(Session["UserId"]),
                DeleteData=Convert.ToBoolean(1)
            };
            db.tbl_OtherIncomes.InsertOnSubmit(sale1other);
            db.SubmitChanges();

            foreach (var line in objOtherIncomeDetails.ListOfOtherIncome)
            {
                TempData["ID"] = sale1other.Id1;
                tbl_OtherIncomeInner3 inner = new tbl_OtherIncomeInner3()
                {
                   
                ItemName = line.ItemName,
                    SalePrice = line.SalePrice,
                    Id1 = sale1other.Id1,
                    ItemAmount = line.ItemAmount,
                    Qty = line.Qty,
                     Company_ID = Convert.ToInt32(Session["UserId"]),
                    DeleteData = Convert.ToBoolean(1)
                };
                db.tbl_OtherIncomeInner3s.InsertOnSubmit(inner);
                db.SubmitChanges();
        }
            //return Json(data: new {msg= "Data sucessfully inserted", status=true}, JsonRequestBehavior.AllowGet);
            //return Json(data: new { success = true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });
             return RedirectToAction("GetReport");
    }


        [HttpGet]
        public ActionResult otcategory()
		{
			return View();
		}
        [HttpPost]
        public ActionResult otcategory(tbl_otherIncomeCaategory emp)
        {      
              var tb = db.tbl_otherIncomeCategorySelect("Insert", null,emp.OtherIncome, Convert.ToInt32(Session["UserId"]));
             db.SubmitChanges();
            return RedirectToAction("Index");     
        }
        //     [HttpPost]
        //     public ActionResult AddOrEdit(tbl_OtherIncome emp,int id = 0)
        //     {
        //if (id == 0)
        //{
        //	var tb = db.tbl_OtherIncomeSelect("Insert",  null,emp.IncomeCategory, emp.Date, null, null, null, emp.RoundOFF, emp.total, emp.Received, emp.Balance, null, null, null, null, null, null, null);
        //	db.SubmitChanges();
        //	return RedirectToAction("Index");
        //}
        //else
        //{
        //	var tb = db.tbl_OtherIncomeSelect("Update", id, emp.IncomeCategory, emp.Date, null,null, null, emp.RoundOFF, emp.total, emp.Received, emp.Balance, null, null, null, null, null, null, null);
        //	db.SubmitChanges();
        //	return RedirectToAction("Index");
        //}
        //  }		



        public ActionResult ViewerEvent1()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult ReportAll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport1()
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.InvoiceDate, b.InvoiceID, b.PartyName, b.PaymentType, b.Total, b.Received, b.RemainingBal,b.DeleteData, b.Status,b.Company_ID from tbl_SaleInvoice as b,tbl_CompanyMaster as c where b.Company_ID = '" + MainLoginController.companyid1 + "' and c.CompanyID='" + MainLoginController.companyid1 + "' and b.DeleteData = '1'");
           string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.ItemName,b.Qty,b.ItemAmount,b.Company_ID from tbl_OtherIncomeInner3 as b,tbl_CompanyMaster as c where b.Company_ID = '" + MainLoginController.companyid1 + "' and c.CompanyID='" +MainLoginController.companyid1 + "' and b.DeleteData = '1'");

            //string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "OtherIncomeItemData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/OtherIncomeItemData.mrt"));
            report.RegData("OtherIncomeItemData", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
			var tb = db.tbl_OtherIncomeSelect("Delete", id,null,null,null,null,null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
			db.SubmitChanges();
			return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
		}
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
     
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            string Query = string.Format("select c.AdditinalFeild1,c.AdditinalFeild2,c.AdditinalFeild3,a.Id1,a.ItemName,a.SalePrice,a.DeleteData,a.Qty,a.ItemAmount,b.Id1,b.IncomeCategory,b.Date,b.Balance,b.Received,b.Status,b.Company_ID,b.DeleteData,c.CompanyName,c.CompanyID,c.Address,c.PhoneNo,c.EmailID,c.AddLogo,c.GSTNumber  from tbl_OtherIncomeInner3 as a,tbl_OtherIncome as b,tbl_CompanyMaster as c where a.Id1=" + id + " and b.Id1=" + id+" and c.CompanyID='" + MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "' and b.DeleteData='1' and a.DeleteData='1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "OtherIncome");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/OtherIncomeReport1.mrt"));
            report.RegData("OtherIncome", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        public ActionResult Report()
        {
            return View();
        }
    }
}