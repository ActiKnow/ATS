using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Global
{
   public class Constants
    {
        public const string POST = "POST";
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
        public const string USERID = "USERID";
        public const string ADMIN = "Admin";
        public const string EMPLOYEE = "Employee";
        public const string CANDIDATE = "Candidate";
        public const string OPTION = "Option";
        public const string BOOL = "Bool";
        public const string TEXT = "Text";
        public const string ROLE = "Role";
        public const string STATUS = "Status";
        public const string ACTIVE = "Active";
        public const string INACTIVE = "InActive";
        public static string ApiBaseUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(); }
        }

    }    
}
