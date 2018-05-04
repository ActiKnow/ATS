using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.CommonModel
{
    public class QuestionBankModel : BaseModel
    {
        public QuestionBankModel()
        {
            this.QuestionOptionMappings = new List<QuestionOptionMapping>();
            this.TestQuestionMappings = new List<TestQuestionMapModel>();
            this.UserAttemptedHistories = new List<UserAttemptHistoryModel>();
        }
        [Key]
        public System.Guid QId { get; set; }
        
        public string Description { get; set; }
        public string QuesTypeId { get; set; }
        public string LevelTypeId { get; set; }
        public int DefaultMark { get; set; }
    
        public virtual List<QuestionOptionMapping> QuestionOptionMappings { get; set; }

        public virtual List<TestQuestionMapModel> TestQuestionMappings { get; set; }

        public virtual List<UserAttemptHistoryModel> UserAttemptedHistories { get; set; }
    }
}
