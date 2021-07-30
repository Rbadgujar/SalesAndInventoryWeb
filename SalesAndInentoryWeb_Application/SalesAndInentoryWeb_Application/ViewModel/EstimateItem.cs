using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class EstimateItem
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public double SalePrice { get; set; }
        public string TaxForSale { get; set; }
        public string Discount { get; set; }
        public int Qty { get; set; }
        public Double DiscountAmount { get; set; }
        public double SaleTaxAmount { get; set; }
        public double ItemTotal { get; set; }
        public int ItemAmount { get; set; }
    }
}