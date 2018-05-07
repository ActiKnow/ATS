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
        public string QuesTypeId { get; set; }
        public string LevelTypeId { get; set; }
        public string CategoryTypeId { get; set; }
        public int DefaultMark { get; set; }
        public string AnsText { get; set; }
        public List<QuestionOptionModel> Options { get; set; }
        public QuestionOptionMapModel MapOptions { get; set; }
    }
}
