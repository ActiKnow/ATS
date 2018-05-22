using System;
using System.Collections.Generic;
using System.Text;
using ATS.Core.Global;

namespace ATS.Core.Model
{
   public class BaseModel
    {
        public int StatusId { get; set; } =(int) CommonType.ACTIVE;
        public System.DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
