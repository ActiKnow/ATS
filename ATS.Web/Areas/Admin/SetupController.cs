using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.Web.Areas.Admin
{
    public class SetupController : Controller
    {
        // GET: Admin/Setup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Role()
        {
            return View();
        }

        public ActionResult TypeSetup()
        {

            return View();
        }
    }
}