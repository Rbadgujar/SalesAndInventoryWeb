using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SalesAndInentoryWeb_Application.Models;


namespace SalesAndInentoryWeb_Application.Data_Access
{
	public class Bank
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["idealtec_inventoryEntities"].ConnectionString);
		// SqlConnection con = new SqlConnection("data source=103.83.81.80;initial catalog=idealtec_inventory;user id=idealtec_inventory;password=Mpiti@123;MultipleActiveResultSets=True;App=EntityFramework");

		idealtec_inventoryEntities10 dc = new idealtec_inventoryEntities10();

		public DataSet show_data()
		{

			

			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Action", "Select");
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}

		public List<BankAccountSelect_Result> SelectBank()
		{
			var daac = dc.BankAccountSelect("Select", null, null, null, null, null, null,null).ToList();
			return daac.ToList();

		}
		

		public void AddOrEdit(tbl_BankAccount bk)
		{
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Action", "Insert");
			//cmd.Parameters.AddWithValue("@ID", id);
			cmd.Parameters.AddWithValue("@BankName", bk.BankName);
			cmd.Parameters.AddWithValue("@AccountName", bk.AccountName);
			cmd.Parameters.AddWithValue("@AccountNo", bk.AccountNo);
			//cmd.Parameters.AddWithValue("@OpeningBal", bk.OpeningBal);
			cmd.Parameters.AddWithValue("@Date", bk.Date);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}
		
	}
}