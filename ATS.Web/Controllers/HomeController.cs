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
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            UserCredentialModel userCredential = new UserCredentialModel();
            if (ReturnUrl != null)
                userCredential.ReturnUrl = ReturnUrl;
            else
                userCredential.ReturnUrl = "";

            return View(userCredential);
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            ViewBag.Message = "Registration page.";

            return View();
        }

        [AllowAnonymous]
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

                            FormsAuthentication.SetAuthCookie(Session[Constants.USERID].ToString(), userCredential.RememberMe);

                            if (!string.IsNullOrEmpty(userCredential.ReturnUrl))
                                return RedirectToAction("SetUserCredential", "Account", new { @ReturnUrl = userCredential.ReturnUrl });
                            else
                                return RedirectToAction("SetUserCredential", "Account");
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

        
        public ActionResult SetUserCredential(string ReturnUrl)
        {
            ApiResult apiResult = new ApiResult(false, new List<string> { "Invalid Credentials." });
            try
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    var url = System.Web.HttpUtility.UrlDecode(ReturnUrl);
                    return new RedirectResult(url);
                }
                else
                {
                    var RoleType = (CommonType)Session[Constants.ROLE];

                    if (RoleType == CommonType.ADMIN)
                        return RedirectToAction("Index", "Dashboard", new { @Area = "Admin" });
                    //else if (RoleType == Constants.EMPLOYEE)
                    //    return RedirectToAction("Index", "Dashboard", new { @Area = "Employee" });
                    //else if (RoleType == Constants.CANDIDATE)
                    //    return RedirectToAction("Index", "Dashboard", new { @Area = "Candidate" });
                    else
                        apiResult = new ApiResult(false, new List<string> { "Role is not defiend" });
                }

            }
            catch (Exception ex)
            {
                apiResult = new ApiResult( false, new List<string> { ex.GetBaseException().Message });
                ViewBag.Error = apiResult.Message;
            }
            ViewBag.Error = apiResult.Message[0];
            return View("Index");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

      
    }
}