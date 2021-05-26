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
    
    public partial class tbl_DeliveryChallanInner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DeliveryChallanInner()
        {
            this.tbl_DeliveryChallan1 = new HashSet<tbl_DeliveryChallan>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public string BasicUnit { get; set; }
        public string ItemCode { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string TaxForSale { get; set; }
        public Nullable<double> SaleTaxAmount { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> freeQty { get; set; }
        public string Discount { get; set; }
        public Nullable<double> DiscountAmount { get; set; }
        public Nullable<int> ItemAmount { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public Nullable<int> ChallanNo { get; set; }
        public Nullable<double> CGST { get; set; }
        public Nullable<double> SGST { get; set; }
        public Nullable<double> IGST { get; set; }
        public Nullable<double> CalTotal { get; set; }
    
        public virtual tbl_DeliveryChallan tbl_DeliveryChallan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DeliveryChallan> tbl_DeliveryChallan1 { get; set; }
        public virtual tbl_ItemMaster tbl_ItemMaster { get; set; }
    }
}
