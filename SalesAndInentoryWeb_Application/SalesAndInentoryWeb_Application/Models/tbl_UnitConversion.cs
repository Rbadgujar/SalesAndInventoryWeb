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
    
    public partial class tbl_UnitConversion
    {
        public int UnitConversionID { get; set; }
        public string BasicUnit { get; set; }
        public string SecondaryUnit { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<bool> DeleteData { get; set; }
        public Nullable<int> Company_ID { get; set; }
    }
}
