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
    
    public partial class tbl_Paymentout
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public Nullable<int> ReceiptNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
        public Nullable<double> Paid { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> Total { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> PartiesID { get; set; }
        public Nullable<bool> CompanyID { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public string TableName { get; set; }
        public string Status { get; set; }
        public Nullable<int> Company_ID { get; set; }
    
        public virtual tbl_PartyMaster tbl_PartyMaster { get; set; }
    }
}
