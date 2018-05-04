using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }       

        public ActionResult Registration()
        {
            ViewBag.Message = "Registration page.";

            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin()
        {
            return View();
        }
    }
}