﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Model
{
   public class Constants
    {
        public const string POST = "POST";
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
        public const string USERID = "USERID";
        public const string ROLE = "ROLE";
        public static string ApiBaseUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(); }
        }

        public const string ADMIN = "Admin";
        public const string EMPLOYEE = "Employee";
        public const string CANDIDATE = "Candidate";
    }    
}
