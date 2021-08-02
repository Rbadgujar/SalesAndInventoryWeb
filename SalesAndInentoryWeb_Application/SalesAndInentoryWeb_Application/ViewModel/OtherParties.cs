using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class OtherParties
    {
     public string ExpenseCategory { get; set; }
        public DateTime Date { get; set; }
        public double Paid { get; set; }
        public double Balance { get; set; }
        public int ID1 { get; set; }
        public string Status { get; set; }
        public double Total { get; set;  }
        public string CategoryID { get; set; }
        public bool DeleteData { get; set; }
        public List<ItemDetails> ListOfOtherIncomeDetails { get; set; }
    }
}