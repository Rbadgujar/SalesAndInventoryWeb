using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.Models
{
    public class Company
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string RefferalCode { get; set; }
        public string BusinessType { get; set; }
        public string OwnerName { get; set; }
        public string GSTNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public byte[] AddLogo { get; set; }
        public byte[] Signature { get; set; }
    }
}