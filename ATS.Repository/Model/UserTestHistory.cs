using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
  public  class UserTestHistory
    {
        public UserTestHistory()
        {
            this.UserAttemptedHistories = new List<UserAttemptedHistory>();
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

        public virtual TestBank TestBank { get; set; }
        public virtual List<UserAttemptedHistory> UserAttemptedHistories { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
