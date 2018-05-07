using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Core.Model;

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
        public ActionResult ValidateLogin(UserCredentialModel userCredential)
        {
            ApiResult apiResult = null;
            try
            {
                if (userCredential != null)
                {
                    apiResult = ApiConsumers.UserApiConsumer.ValidateUser(userCredential);

                    if (apiResult != null)
                    {
                        if (apiResult.Status && apiResult.Data != null)
                        {
                            return RedirectToAction("SetUserCredential");
                        }
                    }
                    else
                    {
                        apiResult = new ApiResult(false ,"Error Occured." );
                    }
                }
                else
                {
                    apiResult = new ApiResult(false,"Error Occured.");
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false,ex.GetBaseException().Message );
            }

            ViewBag.Error = apiResult.Message;

            return View("Index", userCredential);
        }
    }
}