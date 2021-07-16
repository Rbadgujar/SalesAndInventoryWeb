using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.ViewModel
{
    public class ItemDetails
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int SalePrice { get; set; }

      public int OrderNo { get; set; }
    }
}