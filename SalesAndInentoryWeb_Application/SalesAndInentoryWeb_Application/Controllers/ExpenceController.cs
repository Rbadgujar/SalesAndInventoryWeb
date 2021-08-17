using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesAndInentoryWeb_Application.Models;
using System.Data.Entity;
using SalesAndInentoryWeb_Application.ViewModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Mvc;
namespace SalesAndInentoryWeb_Application.Controllers
{
    public class ExpenceController : Controller
    {
		CompanyDataClassDataContext db = new CompanyDataClassDataContext();
		// GET: Expence
		public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Expencescategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Expencescategory(tbl_ExpenseCategory emp)
        {
            var tb = db.tbl_ExpenseCategorySelect("Insert1", null, emp.CategoryName, Convert.ToInt32(Session["UserId"]));
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExpenceData()
        { 
			var tb = db.tbl_ExpensesSelect("Select", null, null, null, null, null, null,null, null, null, null, null, null, Convert.ToInt32(Session["UserId"])).ToList();
			return Json(new { data = tb }, JsonRequestBehavior.AllowGet);
    	}

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            tbl_Expense bt = new tbl_Expense();
            bt.ListOfAccounts = ListOfItems();
            bt.ListOfCategory = ListOfCategorys();
            return View(bt);       
            
        }
       
        private static List<SelectListItem> ListOfItems()
        {
            string sql;
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT ItemName FROM tbl_ItemMaster where  Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1'");
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
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        private static List<SelectListItem> ListOfCategorys()
        {
            string sql;
            List<SelectListItem> items1 = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT CategoryName FROM tbl_ExpenseCategory where Company_ID='" + MainLoginController.companyid1 + "' and DeleteData='1'");
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
                                Text = sdr["CategoryName"].ToString(),
                                Value = sdr["CategoryName"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items1;
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
        public int losan, Id1 = 0;
        [HttpPost]
        public ActionResult AddOrEdit(OtherParties objExpensesDetails)
        {
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            tbl_Expense sale = new tbl_Expense()
            {
                ExpenseCategory = objExpensesDetails.ExpenseCategory,
                Paid = objExpensesDetails.Paid,
                Balance = objExpensesDetails.Balance,
                Date = objExpensesDetails.Date,
                Total = objExpensesDetails.Total ,
             Company_ID= Convert.ToInt32(Session["UserId"]),
             DeleteData=Convert.ToBoolean(1)
            };
            db.tbl_Expenses.InsertOnSubmit(sale);
            db.SubmitChanges();

            foreach (var item in objExpensesDetails.ListOfOtherIncomeDetails)
            {
                TempData["ID"] = sale.ID1;
                tbl_ExpensesInner inner = new tbl_ExpensesInner()
                {

                    ItemName = item.ItemName,
                    SalePrice = item.SalePrice,
                    ID1 = sale.ID1,
                    ItemAmount = item.ItemAmount,
                    Qty = item.Qty,
                   Company_ID= Convert.ToInt32(Session["UserId"]),
                   DeleteData=Convert.ToBoolean(1)
                };
                db.tbl_ExpensesInners.InsertOnSubmit(inner);
                db.SubmitChanges();
              
            }
            return RedirectToAction("GetReport");
            // return Json(data: new {success=true, message = "Insert Data Successfully", JsonRequestBehavior.AllowGet });

        }
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
            string Query = string.Format("select c.CompanyID,c.CompanyName,c.Address,c.AddLogo,c.PhoneNo,c.GSTNumber,c.EmailID,b.BillDate,b.Company_ID, b.BillNo, b.PartyName, b.PaymentType, b.Total, b.Paid, b.RemainingBal,b.DeleteData, b.Status from tbl_PurchaseBill as b,tbl_CompanyMaster as c where c.CompanyID = '"+ MainLoginController.companyid1  + "' and b.Company_ID = '" + MainLoginController.companyid1 + "' and  b.DeleteData = '1'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);
            DataSet dataSet = new DataSet("productsDataSet");
            adapter.Fill(dataSet, "PurchaseBillData");
            StiReport report = new StiReport();
            report.Load(Server.MapPath("~/Content/Report/PurchaseBillData.mrt"));
            report.RegData("PurchaseBillData", dataSet);
            return StiMvcViewer.GetReportResult(report);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var tb = db.tbl_ExpensesSelect("Delete", id, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            db.SubmitChanges();
            return Json(new { success = true, message = "Delete Data Successfully" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            int id = Convert.ToInt32(TempData["ID"]);
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;

            string Query = string.Format("SELECT a.CompanyID,a.CompanyName, a.Address, a.PhoneNo, a.EmailID,a.GSTNumber,a.AddLogo, a.AdditinalFeild1,a.AdditinalFeild2,a.AdditinalFeild3,b.ID1, b.Date, b.ExpenseCategory, b.Paid,b.Balance,b.DeleteData,b.Status,b.Total,b.Company_ID,c.ID1,c.ItemName,c.SalePrice,c.Qty,c.ItemAmount,c.DeleteData,c.Company_ID FROM tbl_CompanyMaster  as a, tbl_Expenses as b,tbl_ExpensesInner as c where b.ID1=" + id + " and c.ID1=" + id + " and b.DeleteData='1' and c.DeleteData='1' and a.CompanyID='"+ MainLoginController.companyid1 + "' and b.Company_ID='" + MainLoginController.companyid1 + "'");
            SqlDataAdapter adapter = new SqlDataAdapter(Query, constr);

            DataSet dataSet = new DataSet("productsDataSet");

            adapter.Fill(dataSet, "Expences");



            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Content/Report/ExpenceReport.mrt"));

            report.RegData("Expences", dataSet);
            return StiMvcViewer.GetReportResult(report);

        }
        public ActionResult Report()
        {
            return View();
        }
    }
}