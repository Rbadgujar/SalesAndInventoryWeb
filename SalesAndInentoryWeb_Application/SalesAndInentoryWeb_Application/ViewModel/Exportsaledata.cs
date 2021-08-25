using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class Exportsaledata
    {


        public string InvoiceID { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }
        public string ContactNo { get; set; }
        public string PONumber { get; set; }
        public string PODate { get; set; }
        public string InvoiceDate { get; set; }
        public string StateOfSupply { get; set; }
        public string PaymentType { get; set; }
        public string TransportName { get; set; }
        public string DeliveryLocation { get; set; }
        public string VehicleNumber { get; set; }
        public string DeliveryDate { get; set; }
        public string EwayBillNo { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
         public string SGST { get; set; }
        public string Barcode { get; set; }
        public string TotalDiscount { get; set; }
        public string Feild3 { get; set; }
        public string Received { get; set; }
        public string Total { get; set; }
        public string RemainingBal { get; set; }
        public string CalTotal { get; set; }
        public string TaxAmount1 { get; set; }
        public string Status { get; set; }
    }
}