using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class PartyDetailsForPurchase
    {
        public int BillNo { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }
        public string PONo { get; set; }
        public string ContactNo { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Feild4 { get; set; }
        public string StateOfSupply { get; set; }
        public string PaymentType { get; set; }
        public string TransportName { get; set; }
        public string DeliveryLocation { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Barcode { get; set; }
        public int TaxAmount1 { get; set; }
        public int CGST { get; set; }
        public int SGST { get; set; }
        public double Paid { get; set; }
        public double RemainingBal { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }

        public List<ItemPurchase> listpurchasedetail { get; set; }
    }
}