using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("TestbankId")]
        public  TestBank TestBank { get; set; }

        [InverseProperty("UserTestHistory")]
        public  List<UserAttemptedHistory> UserAttemptedHistories { get; set; }

        [ForeignKey("UserId")]
        public  UserInfo UserInfo { get; set; }

       
       
    }
}
