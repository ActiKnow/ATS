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
            if (User.Identity.IsAuthenticated)
            {
                Session[Constants.USERID] = User.Identity.Name;
                return RedirectToAction("SetUserCredential", ReturnUrl);
            }
            else
            {
                UserCredentialModel userCredential = new UserCredentialModel();
                if (ReturnUrl != null)
                    userCredential.ReturnUrl = ReturnUrl;
                else
                    userCredential.ReturnUrl = "";

                return View(userCredential);
            }
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
                            UserInfoModel userInfo = (UserInfoModel)apiResult.Data;

                            Session[Constants.USERID] = userInfo.UserId;
                            Session[Constants.ROLE] = userInfo.RoleTypeValue;

                            FormsAuthentication.SetAuthCookie(Session[Constants.USERID].ToString(), userCredential.RememberMe);

                            if (!string.IsNullOrEmpty(userCredential.ReturnUrl))
                                return RedirectToAction("SetUserCredential", "Home", new { @ReturnUrl = userCredential.ReturnUrl });
                            else
                                return RedirectToAction("SetUserCredential", "Home");
                        }
                    }
                    else
                    {
                        apiResult = new ApiResult(false, new List<string> { "Error Occured" });
                    }
                }
                else
                {
                    apiResult = new ApiResult(false, new List<string> { "Error Occured" });
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
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
                    return RedirectToAction("Index", "Dashboard", new { @Area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                apiResult = new ApiResult(false, new List<string> { ex.GetBaseException().Message });
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