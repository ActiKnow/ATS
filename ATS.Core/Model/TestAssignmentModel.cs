using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class TestAssignmentModel : BaseModel
    {
        [Key]
        public System.Guid ID { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid TestBankId { get; set; }
        public decimal MarksObtained { get; set; } = 0;
        public virtual TestBankModel TestBank { get; set; }
        public virtual UserInfoModel UserInfo { get; set; }
    }
}
