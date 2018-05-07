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
        public string QuesTypeValue { get; set; }
        public string CategoryTypeValue { get; set; }
        public string LevelTypeValue { get; set; }
        public List<QuestionOptionModel> Options { get; set; }
        public QuestionOptionMapModel MapOptions { get; set; }
    }
}

