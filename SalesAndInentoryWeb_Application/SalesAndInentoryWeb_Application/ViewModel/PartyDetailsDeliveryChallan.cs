using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class PartyDetailsDeliveryChallan
    {
        public int ChallanNo { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }
        public string ContactNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string StateOfSupply { get; set; }
        public string PaymentType { get; set; }
        public string TransportName { get; set; }
        public string DeliveryLocation { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Barcode { get; set; }
        public double Received { get; set; }
        public double RemainingBal { get; set; }
        public double CalTotal { get; set; }
        public string Status { get; set; }

        public List<ItemPurchase> ListOfDeliveryChallan { get; set; }
    }
}