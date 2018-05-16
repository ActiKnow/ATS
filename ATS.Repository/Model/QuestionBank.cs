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
            this.MappedOptions = new List<QuestionOptionMapping>();
            this.TestQuestionMappings = new List<TestQuestionMapping>();
            this.UserAttemptedHistories = new List<UserAttemptedHistory>();
            //this.Options = new List<QuestionOption>();
        }
        [Key]
        public System.Guid QId { get; set; }
        
        public string Description { get; set; }
        public int QuesTypeValue { get; set; }
        public int LevelTypeValue { get; set; }
        public int CategoryTypeValue { get; set; }
        public int DefaultMark { get; set; }
    
        public List<QuestionOptionMapping> MappedOptions { get; set; }

        public List<TestQuestionMapping> TestQuestionMappings { get; set; }

        public List<UserAttemptedHistory> UserAttemptedHistories { get; set; }

        //public List<QuestionOption> Options { get; set; }
        [ForeignKey("CategoryTypeValue")]
        public virtual TypeDef CategoryType { get; set; }
        [ForeignKey("LevelTypeValue")]
        public virtual TypeDef LevelType { get; set; }
        [ForeignKey("QuesTypeValue")]
        public virtual TypeDef QuestionType { get; set; }
    }
}
