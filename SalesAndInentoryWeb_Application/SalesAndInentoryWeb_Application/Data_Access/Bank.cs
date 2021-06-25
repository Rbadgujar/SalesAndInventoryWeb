using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SalesAndInentoryWeb_Application.Models;
using System.Web.Mvc;
using System.Data.Common;

namespace SalesAndInentoryWeb_Application.Data_Access
{
	public class Bank
	{

		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["idealtec_inventoryEntities10"].ToString());
		public DataSet show_data()
		{
			string qry = string.Format("select * from tbl_BankAccount where DeleteData='1'");
			SqlCommand cmd = new SqlCommand(qry, con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;

			//SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			//cmd.CommandType = CommandType.StoredProcedure;
			//cmd.Parameters.AddWithValue("@Action", "select");
			////cmd.Parameters.AddWithValue("@ID", null);
			////cmd.Parameters.AddWithValue("@BankName", null);
			////cmd.Parameters.AddWithValue("@AccountName", null);
			////cmd.Parameters.AddWithValue("@AccountNo", null);
			////cmd.Parameters.AddWithValue("@OpeningBal", null);
			////cmd.Parameters.AddWithValue("@Date", null);
			//SqlDataAdapter da = new SqlDataAdapter(cmd);
			//DataSet ds = new DataSet();
			//da.Fill(ds);
			//return ds;
		 }

		public void InsertData(tbl_BankAccount objcust)
		{
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@BankName", objcust.BankName);
			cmd.Parameters.AddWithValue("@AccountName", objcust.AccountName);
			cmd.Parameters.AddWithValue("@AccountNo", objcust.AccountNo);
			cmd.Parameters.AddWithValue("@OpeningBal", objcust.OpeningBal);
			cmd.Parameters.AddWithValue("@Date", objcust.Date);
			cmd.Parameters.AddWithValue("@Action", "Insert");
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}

		public void Updatedata(tbl_BankAccount objcust)
		{
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ID", objcust.ID);
			cmd.Parameters.AddWithValue("@BankName", objcust.BankName);
			cmd.Parameters.AddWithValue("@AccountName", objcust.AccountName);
			cmd.Parameters.AddWithValue("@AccountNo", objcust.AccountNo);
			cmd.Parameters.AddWithValue("@OpeningBal", objcust.OpeningBal);
			cmd.Parameters.AddWithValue("@Date", objcust.Date);
			cmd.Parameters.AddWithValue("@Action", "Update");
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}

		public DataSet SelectID(int id)
		{
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ID", id);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}

		public void DeleteData(int id)
		{
			SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ID", id);
			cmd.Parameters.AddWithValue("@Action", "Delete");
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
		}
	}
}

		

