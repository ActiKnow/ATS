using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public  QuestionBank QuestionBank { get; set; }
        [ForeignKey("History_Id")]
        public  UserTestHistory UserTestHistory { get; set; }
    }
}
