using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Model
{
   public class UserFeedbackModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Feedback { get; set; }
        public Guid UserId { get; set; }        
        public decimal Reating { get; set; }
        public bool ReadStatus { get; set; } = false;
        public UserInfoModel userInfoModel { get; set; }
        public string StatusDescription { get; set; }
    }
}
