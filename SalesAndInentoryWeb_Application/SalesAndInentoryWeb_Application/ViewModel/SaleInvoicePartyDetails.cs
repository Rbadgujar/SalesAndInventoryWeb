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
        public string ContactNo { get; set; }
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string StateOfSupply { get; set; }
        public string PaymentType { get; set; }
        public string TransportName { get; set; }
        public string DeliveryLocation { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string EwayBillNo { get; set; }
        public string Barcode { get; set; }
        public string Feild3 { get; set; }
        public double Received { get; set; }       
        public double RemainingBal { get; set; }
        public double CalTotal { get; set; }
        public int TaxAmount1 { get; set; }
        public string Status { get; set; }
        public List<SaleInvoiceItemDetails> SaleInvoiceItemDetails { get; set; }
    }
}