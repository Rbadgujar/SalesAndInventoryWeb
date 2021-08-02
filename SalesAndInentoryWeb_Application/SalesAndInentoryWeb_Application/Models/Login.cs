using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesAndInentoryWeb_Application.Models
{
    public class Login
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
    }
}