using ATS.Core.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
    public class QuestionBankModel : BaseModel
    {
        public QuestionBankModel()
        {
            Options = new List<QuestionOptionModel>();
        }

        public System.Guid QId { get; set; }

        public string Description { get; set; }
        public int QuesTypeValue { get; set; }
        public int LevelTypeValue { get; set; }
        public int CategoryTypeValue { get; set; }
        public string QuesTypeDescription { get; set; }
        public string LevelTypeDescription { get; set; }
        public string CategoryTypeDescription { get; set; }
        public int DefaultMark { get; set; }
        public string AnsText { get; set; }
        public List<QuestionOptionModel> Options { get; set; }
        public List<QuestionOptionMapModel> MappedOptions { get; set; }
        public TestQuestionMapModel MappedQuestion { get; set; }
    }
}

