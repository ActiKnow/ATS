using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
  public  class UserTestHistoryModel
    {
        public UserTestHistoryModel()
        {
            this.UserAttemptedHistories = new List<UserAttemptHistoryModel>();
        }
        [Key]
        public System.Guid HistoryId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid TestbankId { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public Nullable<System.DateTime> LastUsedDate { get; set; }
        public bool IsFinished { get; set; }
        public System.DateTime ReusableDate { get; set; }
        public Nullable<decimal> TotalDuration { get; set; }

        public virtual TestBankModel TestBank { get; set; }
        public virtual List<UserAttemptHistoryModel> UserAttemptedHistories { get; set; }
        public virtual UserInfoModel UserInfo { get; set; }
    }
}
