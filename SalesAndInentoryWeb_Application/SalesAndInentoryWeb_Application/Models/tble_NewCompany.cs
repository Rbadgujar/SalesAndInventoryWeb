//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesAndInentoryWeb_Application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tble_NewCompany
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string ReferralCode { get; set; }
        public byte[] Image1 { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
    }
}
