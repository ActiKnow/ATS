using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.Web.Helper
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine() : base()
        {
            base.AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            base.AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml"
            };

            base.AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml",
                 "~/Areas/{2}/Views/{1}/Partials/{0}.cshtml", "~/Areas/{2}/Views/{1}/Partials/{0}.vbhtml",
                "~/Areas/{2}/Views/Shared/Partials/{0}.cshtml", "~/Areas/{2}/Views/Shared/Partials/{0}.vbhtml"
            };

            base.ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            };

            base.MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            };

            base.PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
                "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml",
                "~/Views/{1}/Partials/{0}.cshtml", "~/Views/{1}/Partials/{0}.vbhtml",
                "~/Views/Shared/Partials/{0}.cshtml", "~/Views/Shared/Partials/{0}.vbhtml"
            };
        }
    }
}