using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
    public class TestQuestionMapping
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid TestBankId { get; set; }
        public System.Guid QId { get; set; }
        public decimal Marks { get; set; }

        public virtual QuestionBank QuestionBank { get; set; }
        public virtual TestBank TestBank { get; set; }
    }
}
