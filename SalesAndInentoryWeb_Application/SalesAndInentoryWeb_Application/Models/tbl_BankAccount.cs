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
    
    public partial class tbl_BankAccount
    {
        public int ID { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public Nullable<double> OpeningBal { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public int Company_ID { get; set; }
    }
}
