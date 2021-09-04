using SalesAndInentoryWeb_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class PosController : Controller
    {
        CompanyDataClassDataContext db = new CompanyDataClassDataContext();
        // GET: Pos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Data(string category)
        {
            if (category == "All")
            {
                var getdata = db.tbl_ItemMasterSelect("Select", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 4, null, null, null, null).ToList();
                return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
            }
            var getdata1 = db.tbl_ItemMasterSelect("Categorywise", null, null, null, null, null, null,category, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 4, null, null, null, null).ToList();
            return Json(new { data = getdata1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Data1(int id)
        {
            var getdata = db.tbl_ItemMasterSelect("Select", id, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 4, null, null, null, null).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult category1()
        {
            var getdata = db.tbl_CategoryMasterSelect("Select",null, null,4).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult hold()
        {
            var getdata = db.tbl_CategoryMasterSelect("hold", null, null, 4).ToList();
            return Json(new { data = getdata }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFruitName1(string id,string bt)
        {
            if (bt == 1.ToString())
            {
                return Json(GetFruitNameById1(id), JsonRequestBehavior.AllowGet);
            }
            return Json(GetFruitNameById2(id), JsonRequestBehavior.AllowGet);
        }
        private static List<tbl_ItemMaster> GetFruitNameById1(string id)
        {
            string sql;
            List<tbl_ItemMaster> items3 = new List<tbl_ItemMaster>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE ItemID =@Id and DeleteData='1' ");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items3.Add(new tbl_ItemMaster()
                            {
                                ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                //SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                Discount = Convert.ToDouble(sdr["Discount"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }
        int holdnumber;
        public ActionResult SaleInvoice(SaleInvoicePartyDetails obj)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                con.Open();
                string sql1 = string.Format("select max(id) from hold");
                SqlCommand cmd1 = new SqlCommand(sql1, con);
               string count=cmd1.ExecuteScalar().ToString();

               if(count=="")
                {
                    holdnumber = 1;
                }
               else
                {
                    holdnumber =Convert.ToInt32(count)+1;
                }

               if(obj.Received==0)
                {
                    obj.Received = holdnumber;
                }
                else
                {
                    string sql2 = string.Format("Delete from hold where Id='"+obj.Received+"'");
                    SqlCommand cmd3 = new SqlCommand(sql2, con);
                    cmd3.ExecuteNonQuery();
                    holdnumber =Convert.ToInt32(obj.Received);
                }

                foreach (var item in obj.SaleInvoiceItemDetails1)
                {
                    string sql = string.Format("INSERT INTO [Hold]([Id],[PartyName],[ContactNo],[Total],[Alltax],[ItemName],[Rate],[ItemTotal],[Qty] ,[Company_Id],[ItemTax],holdid)VALUES(" + holdnumber+",'" + obj.PartyName+"','"+obj.ContactNo+"',"+obj.CalTotal+","+obj.TaxAmount1+",'"+item.ItemName+"' ,"+item.saleprice+","+item.ItemTotal+","+item.qty+",4,"+item.tax+","+Convert.ToInt32(obj.Received)+")");
                    SqlCommand cmd = new SqlCommand(sql, con);                  
                    cmd.ExecuteNonQuery();
                }
               
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        private static List<tbl_ItemMaster> GetFruitNameById2(string id)
        {
            string sql;
            List<tbl_ItemMaster> items3 = new List<tbl_ItemMaster>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM tbl_ItemMaster WHERE Barcode =@Id or ItemName=@Id and DeleteData='1' ");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items3.Add(new tbl_ItemMaster()
                            {
                                ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                                ItemName = sdr["ItemName"].ToString(),
                                SalePrice = Convert.ToDouble(sdr["SalePrice"]),
                                TaxForSale = sdr["TaxForSale"].ToString(),
                                //SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                Discount = Convert.ToDouble(sdr["Discount"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items3;
        }


        public JsonResult gethold(string id)
        {
           
            return Json(gethold1(id), JsonRequestBehavior.AllowGet);
        }
        private static List<Tempdata> gethold1(string id)
        {
            string sql;
            List<Tempdata> items4 = new List<Tempdata>();
            string constr = ConfigurationManager.ConnectionStrings["idealtec_inventoryConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                sql = string.Format("SELECT * FROM Hold WHERE id =@Id ");
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items4.Add(new Tempdata()
                            {
                                PartyName = sdr["PartyName"].ToString(),
                                ContactNo = sdr["ContactNo"].ToString(),
                                total = sdr["Total"].ToString(),
                                totaltax = sdr["Alltax"].ToString(),
                                //SaleTaxAmount = Convert.ToDouble(sdr["SaleTaxAmount"].ToString()),
                                ItemName = sdr["ItemName"].ToString(),
                                qty = Convert.ToInt32(sdr["Qty"].ToString()),
                                saleprice = Convert.ToInt32(sdr["Rate"].ToString()),
                                ItemTotal = sdr["ItemTotal"].ToString(),
                                tax = sdr["ItemTax"].ToString(),
                                holdid =Convert.ToInt32(sdr["holdid"].ToString())
                            });
                        }
                    }
                    con.Close();
                }
            }
            return items4;
        }



    }
}