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
    
    public partial class tbl_OtherIncome
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_OtherIncome()
        {
            this.tbl_OtherIncomeInner1 = new HashSet<tbl_OtherIncomeInner1>();
            this.tbl_OtherIncomeInner3 = new HashSet<tbl_OtherIncomeInner3>();
        }
    
        public int Id { get; set; }
        public string IncomeCategory { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string paymentType { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Nullable<double> RoundOFF { get; set; }
        public Nullable<double> total { get; set; }
        public string AdditionalFeild1 { get; set; }
        public string Additional2 { get; set; }
        public string Additional3 { get; set; }
        public string Additional4 { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<double> Received { get; set; }
        public Nullable<double> Balance { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OtherIncomeInner1> tbl_OtherIncomeInner1 { get; set; }
        public virtual tbl_OtherIncome tbl_OtherIncome1 { get; set; }
        public virtual tbl_OtherIncome tbl_OtherIncome2 { get; set; }
        public virtual tbl_otherIncomeCaategory tbl_otherIncomeCaategory { get; set; }
        public virtual tbl_otherIncomeCaategory tbl_otherIncomeCaategory1 { get; set; }
        public virtual tbl_otherIncomeCaategory tbl_otherIncomeCaategory2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OtherIncomeInner3> tbl_OtherIncomeInner3 { get; set; }
    }
}
