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
    
    public partial class tbl_MakePayment
    {
        public int ID { get; set; }
        public Nullable<double> PrincipleAmount { get; set; }
        public Nullable<double> InterestAmount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> TotalAmount { get; set; }
        public string PaidFrom { get; set; }
        public string AccountName { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }

        public virtual tbl_LoanBank tbl_LoanBank { get; set; }
    }
}
