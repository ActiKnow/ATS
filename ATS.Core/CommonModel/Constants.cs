using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.CommonModel
{
   public class Constants
    {
        public const string Post = "POST";
        public const string Get = "GET";
        public const string Put = "PUT";
        public const string Delete = "DELETE";
        public static string ApiBaseUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(); }
        }
    }    
}
