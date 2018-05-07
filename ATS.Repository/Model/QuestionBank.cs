using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
    public class QuestionBank : BaseModel
    {
        public QuestionBank()
        {
            this.QuestionOptionMappings = new List<QuestionOptionMapping>();
            this.TestQuestionMappings = new List<TestQuestionMapping>();
            this.UserAttemptedHistories = new List<UserAttemptedHistory>();
        }
        [Key]
        public System.Guid QId { get; set; }
        
        public string Description { get; set; }
        public string QuesTypeId { get; set; }
        public string LevelTypeId { get; set; }
        public string CategoryTypeId { get; set; }
        public int DefaultMark { get; set; }
    
        public virtual List<QuestionOptionMapping> QuestionOptionMappings { get; set; }

        public virtual List<TestQuestionMapping> TestQuestionMappings { get; set; }

        public virtual List<UserAttemptedHistory> UserAttemptedHistories { get; set; }
    }
}
