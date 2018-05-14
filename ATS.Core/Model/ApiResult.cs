using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Model
{
   public class ApiResult
    {
        public bool Status { get; set; }
        public List<string> Message { get; set; }
        public object Data { get; set; }

        public ApiResult(bool status, List<string> message=null, object data = null)
        {
            this.Message = new List<string>();
            if (message != null)
            {
                foreach (var newMessage in message)
                {
                    this.Message.Add(newMessage);
                }
            }
            this.Status = status;
            this.Data = data;
        }

       

        public ApiResult()
        {
            this.Message = new List<string>();
        }

        public static ApiResult operator+(ApiResult first, ApiResult second)
        {
            if (second != null)
            {
                first.Status = second.Status;

                for (var i=0;i<second.Message.Count;i++)
                {
                    first.Message.Add(second.Message[i]);
                }

                first.Data = second.Data;
            }
            return first;
        }
    }
}
