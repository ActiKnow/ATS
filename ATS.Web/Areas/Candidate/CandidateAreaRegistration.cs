﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.Web.Areas.Candidate
{
    public class CandidateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Candidate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Candidate_default",
                "Candidate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}