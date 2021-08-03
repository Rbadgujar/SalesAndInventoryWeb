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
    
    public partial class tbl_PurchaseBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PurchaseBill()
        {
            this.tbl_PurchaseBillInner = new HashSet<tbl_PurchaseBillInner>();
        }
    
        public int BillNo { get; set; }
        public string PartyName { get; set; }
        public string BillingName { get; set; }
        public string ContactNo { get; set; }
        public string PONo { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public Nullable<System.DateTime> PoDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string StateofSupply { get; set; }
        public string PaymentType { get; set; }
        public string TransportName { get; set; }
        public string DeliveryLocation { get; set; }
        public string VehicleNumber { get; set; }
        public Nullable<System.DateTime> Deliverydate { get; set; }
        public string Description { get; set; }
        public Nullable<double> TransportCharges { get; set; }
        public byte[] Image { get; set; }
        public string Tax1 { get; set; }
        public Nullable<double> CGST { get; set; }
        public Nullable<double> SGST { get; set; }
        public Nullable<double> TaxAmount1 { get; set; }
        public Nullable<int> TotalDiscount { get; set; }
        public Nullable<double> DiscountAmount1 { get; set; }
        public Nullable<double> RoundFigure { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> Paid { get; set; }
        public Nullable<double> RemainingBal { get; set; }
        public string PaymentTerms { get; set; }
        public string Feild1 { get; set; }
        public string Feild2 { get; set; }
        public string Feild3 { get; set; }
        public string Feild4 { get; set; }
        public string Feild5 { get; set; }
        public Nullable<int> PartiesID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public Nullable<int> ID { get; set; }
        public string Barcode { get; set; }
        public Nullable<bool> ComapanyID { get; set; }
        public string ItemCategory { get; set; }
        public Nullable<double> IGST { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> TaxShow { get; set; }
        public Nullable<double> CalTotal { get; set; }
        public Nullable<double> reverschecharges { get; set; }
    
        public virtual tbl_CategoryMaster tbl_CategoryMaster { get; set; }
        public virtual tbl_PartyMaster tbl_PartyMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PurchaseBillInner> tbl_PurchaseBillInner { get; set; }
        public virtual tbl_PurchaseBillInner tbl_PurchaseBillInner1 { get; set; }
    }
}
