using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Candidate
{
    public class DashboardController : BaseController
    {
        // GET: Candidate/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}