using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Web.Controllers;

namespace ATS.Web.Areas.Employee
{
    public class DashboardController : BaseController
    {
        // GET: Employee/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}