using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.CommonModel
{
  public  class UserAttemptHistoryModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid History_Id { get; set; }
        public System.Guid QId { get; set; }
        public string OptionSelected_Id { get; set; }
        public string Description { get; set; }
        public virtual QuestionBankModel QuestionBank { get; set; }
        public virtual UserTestHistoryModel UserTestHistory { get; set; }
    }
}
