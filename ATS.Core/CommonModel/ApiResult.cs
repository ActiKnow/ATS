using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.CommonModel
{
   public class ApiResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResult(string message, bool status, object data = null)
        {
            this.Message = message;
            this.Status = status;
            this.Data = data;
        }
    }
}
