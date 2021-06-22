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
			//SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			//cmd.CommandType = CommandType.StoredProcedure;
			//cmd.Parameters.AddWithValue("@Action", "select");
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
			//SqlCommand cmd = new SqlCommand("BankAccountSelect", con);
			//cmd.CommandType = CommandType.StoredProcedure;
			//cmd.Parameters.AddWithValue("@Action", "select");
			//SqlDataAdapter da = new SqlDataAdapter();
			//da.SelectCommand = cmd;
			//DataSet ds = new DataSet();
			//da.Fill(ds);
			//return ds;
		}
	}
}