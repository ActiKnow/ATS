using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int QuesTypeValue { get; set; }
        public int LevelTypeValue { get; set; }
        public int CategoryTypeValue { get; set; }
        public int DefaultMark { get; set; }
    
        public List<QuestionOptionMapping> QuestionOptionMappings { get; set; }

        public List<TestQuestionMapping> TestQuestionMappings { get; set; }

        public List<UserAttemptedHistory> UserAttemptedHistories { get; set; }

        //[ForeignKey("CategoryTypeValue")]
        //public TypeDef CategoryType { get; set; }

        //[ForeignKey("LevelTypeValue")]
        //public TypeDef LevelType { get; set; }

        //[ForeignKey("QuesTypeValue")]
        //public TypeDef TestType { get; set; }
    }
}
