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
    
    public partial class tbl_ItemServicemasterSelect_Result
    {
        public int ServiceID { get; set; }
        public string ItemName { get; set; }
        public string ItemHSNCOde { get; set; }
        public string Unit { get; set; }
        public string Subunit { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string TaxType { get; set; }
        public string TaxRate { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}
