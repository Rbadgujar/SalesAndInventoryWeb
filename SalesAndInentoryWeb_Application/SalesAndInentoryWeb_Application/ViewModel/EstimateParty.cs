using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesAndInentoryWeb_Application.ViewModel;
namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class EstimateParty
    {
        public string PartyName { get; set; }
        public string BillingAddress { get; set; }
        public string StateOfSupply { get; set; }
        public string ContactNo { get; set; }
        public DateTime Date { get; set; }
        public int RefNo { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public double TaxAmount1 { get; set; }
        public string CategoryID { get; set; }
        public bool DeleteData { get; set; }
        public List<EstimateItem> ListOfEstimateDetails { get; set; }
    }
}