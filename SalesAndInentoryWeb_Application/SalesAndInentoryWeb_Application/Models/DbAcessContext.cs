using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace SalesAndInentoryWeb_Application.Models
{
    public class DbAcessContext:DbContext
    {
        public DbAcessContext():base("idealtec_inventoryConnectionString")
        {

        }
        public DbSet<PartyMaster> Employees { get; set; }

       
    }
}