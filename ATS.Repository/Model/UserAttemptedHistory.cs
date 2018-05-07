using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
  public  class UserAttemptedHistory
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid History_Id { get; set; }
        public System.Guid QId { get; set; }
        public string OptionSelected_Id { get; set; }
        public string Description { get; set; }
        public virtual QuestionBank QuestionBank { get; set; }
        public virtual UserTestHistory UserTestHistory { get; set; }
    }
}
