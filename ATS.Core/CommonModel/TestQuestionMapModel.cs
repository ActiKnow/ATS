using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.CommonModel
{
    public class TestQuestionMapModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid TestBankId { get; set; }
        public System.Guid QId { get; set; }
        public decimal Marks { get; set; }

        public virtual QuestionBankModel QuestionBank { get; set; }
        public virtual TestBankModel TestBank { get; set; }
    }
}
