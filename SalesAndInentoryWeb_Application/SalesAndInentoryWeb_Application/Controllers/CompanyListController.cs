﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class CompanyListController : Controller
    {
        // GET: CompanyList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewCompany()
        {
            return View();
        }
    }
}