using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class otherincome
    {
        public string IncomeCategory { get; set; }
        public DateTime Date { get; set; }
        public double total { get; set; }
        public double Balance { get; set; }
        public int ID1 { get; set; }
        public List<ItemDetails> ListOfOtherIncomeDetails { get; set; }
    }
}