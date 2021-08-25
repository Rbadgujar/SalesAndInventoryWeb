using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class Dashborddata
    {
        public float saleamount { get; set; }
        public float salpending { get; set; }
        public float purchaseamount { get; set; }
        public float purchasepending { get; set; }
        public float PaymentIn { get; set; }
        public float remaining { get; set; }
        public float paymnetout { get; set; }
        public float outremaing { get; set; }
    }
}