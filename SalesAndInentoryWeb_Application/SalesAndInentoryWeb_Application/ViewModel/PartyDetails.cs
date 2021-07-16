using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class PartyDetails
    {
        public int OrderNo { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }

        public List<ItemDetails> listofitemdetail { get; set; }
    }
}