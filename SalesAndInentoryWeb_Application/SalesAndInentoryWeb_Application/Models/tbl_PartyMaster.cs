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
    
    public partial class tbl_PartyMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PartyMaster()
        {
            this.tbl_PaymentIn = new HashSet<tbl_PaymentIn>();
            this.tbl_Paymentout = new HashSet<tbl_Paymentout>();
            this.tbl_PurchaseBill = new HashSet<tbl_PurchaseBill>();
        }
    
        public int PartiesID { get; set; }
        public string PartyName { get; set; }
        public string ContactNo { get; set; }
        public string BillingAddress { get; set; }
        public string EmailID { get; set; }
        public string GSTType { get; set; }
        public string State { get; set; }
        public Nullable<double> OpeningBal { get; set; }
        public Nullable<System.DateTime> AsOfDate { get; set; }
        public string AddRemainder { get; set; }
        public string PartyType { get; set; }
        public string ShippingAddress { get; set; }
        public string PartyGroup { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public string PaidStatus { get; set; }
        public string Customertype { get; set; }
        public string type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PaymentIn> tbl_PaymentIn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Paymentout> tbl_Paymentout { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PurchaseBill> tbl_PurchaseBill { get; set; }
    }
}
