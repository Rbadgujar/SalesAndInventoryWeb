using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class Companymaster
    {

        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string ownername { get; set; }
        public string ReferaleCode { get; set; }
        public string BusinessType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string GSTNumber { get; set; }
        public string OwnerName { get; set; }
        public byte[] Signature { get; set; }
        public byte[] AddLogo { get; set; }
        public string Password { get; set; }
        public string Confropasswrod { get; set; }
        public string Accountno { get; set; }
        public string banlname { get; set; }
        public string IFScCode { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public Nullable<int> Defulatcompany { get; set; }




    }
}