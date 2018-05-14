using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ATS.Core.Global;
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
                            UserInfoModel userInfo=(UserInfoModel)apiResult.Data;

                            Session[Constants.USERID] = userInfo.UserId;
                            Session[Constants.ROLE] = userInfo.RoleTypeValue;

                            return RedirectToAction("SetUserCredential");
                        }
                    }
                    else
                    {
                        apiResult = new ApiResult(false ,new List<string> { "Error Occured" } );
                    }
                }
                else
                {
                    apiResult = new ApiResult(false, new List<string> { "Error Occured" });
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message } );
            }

            ViewBag.Error = apiResult.Message[0];

            return View("Index", userCredential);
        }

        
        public ActionResult SetUserCredential()
        {
            ApiResult apiResult = new ApiResult(false, new List<string> { "Invalid Credentials." });
            try
            { 
                FormsAuthentication.SetAuthCookie(Session[Constants.USERID].ToString(), false);
                var RoleType=(CommonType)Session[Constants.ROLE];

                if (RoleType == CommonType.ADMIN)
                    return RedirectToAction("Index", "Dashboard", new { @Area = "Admin" });
                //else if (RoleType == Constants.EMPLOYEE)
                //    return RedirectToAction("Index", "Dashboard", new { @Area = "Employee" });
                //else if (RoleType == Constants.CANDIDATE)
                //    return RedirectToAction("Index", "Dashboard", new { @Area = "Candidate" });
                else
                    apiResult = new ApiResult(false, new List<string> { "Role is not defiend" });

            }
            catch (Exception ex)
            {
                apiResult = new ApiResult( false, new List<string> { ex.GetBaseException().Message });
                ViewBag.Error = apiResult.Message;
            }
         
            return View("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

      
    }
}