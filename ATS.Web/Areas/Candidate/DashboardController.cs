using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.Web.Areas.Candidate
{
    public class DashboardController : Controller
    {
        // GET: Candidate/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}