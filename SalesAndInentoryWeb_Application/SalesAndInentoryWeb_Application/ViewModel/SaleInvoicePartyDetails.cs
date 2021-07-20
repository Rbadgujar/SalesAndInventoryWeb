using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class SaleInvoicePartyDetails
    {
        public int InvoiceID { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }
        public List<SaleInvoiceItemDetails> SaleInvoiceItemDetails { get; set; }
    }
}