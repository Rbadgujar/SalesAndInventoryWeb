using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesAndInentoryWeb_Application.Controllers
{
    public class BackupController : Controller
    {
        // GET: Backup
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult backuptpdrive()
        {
             return View();
        }
        [HttpPost]
        public ActionResult backuptpdrive(string xyz)
        {
            return View();

        }

        public ActionResult backupdatabase()
        {
            return View();                 
        }
        public ActionResult Restoredatabase()
        {
            return View();
        }
    }
}