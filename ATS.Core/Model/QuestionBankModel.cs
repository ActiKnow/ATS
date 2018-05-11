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
        public System.Guid QuesTypeId { get; set; }
        public System.Guid LevelTypeId { get; set; }
        public System.Guid CategoryTypeId { get; set; }
        public int DefaultMark { get; set; }
        public string AnsText { get; set; }
        public CommonType QuesTypeValue { get; set; }
        public int CategoryTypeValue { get; set; }
        public int LevelTypeValue { get; set; }
        public List<QuestionOptionModel> Options { get; set; }
        public List<QuestionOptionMapModel> MappedOptions { get; set; }
        public TestQuestionMapModel MappedQuestion { get; set; }
    }
}

