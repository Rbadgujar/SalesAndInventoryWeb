using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class SaleInvoiceItemDetails
    {
        public string ItemName { get; set; }
        public int SalePrice { get; set; }
        public string TaxForSale { get; set; }
        public string Discount { get; set; }
        public int Qty { get; set; }
        public double DiscountAmount { get; set; }
        public double SaleTaxAmount { get; set; }
        public double ItemTotal { get; set; }
        public int ItemAmount { get; set; }
    }
}