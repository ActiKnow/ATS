using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Core.CommonModel
{
   public class BaseModel
    {
        public bool Status { get; set; } = false;
        public System.DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
