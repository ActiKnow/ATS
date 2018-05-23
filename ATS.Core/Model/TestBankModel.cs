using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
    public class TestBankModel : BaseModel
    {
        public TestBankModel()
        {
            this.TestAssignments = new List<TestAssignmentModel>();
            this.TestQuestionMappings = new List<TestQuestionMapModel>();
            this.UserTestHistories = new List<UserTestHistoryModel>();
        }
        [Key]
        public System.Guid TestBankId { get; set; }
        public string CategoryTypeDescription { get; set; }
        public int CategoryTypeValue { get; set; }
        public string LevelTypeDescription { get; set; }
        public int LevelTypeValue { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public decimal Duration { get; set; }
        public string TestTypeDescription { get; set; }
        public int TestTypeValue { get; set; }
        public decimal TotalMarks { get; set; }
        public string StatusDescription { get; set; }
        public virtual List<TestAssignmentModel> TestAssignments { get; set; }
        public virtual List<TestQuestionMapModel> TestQuestionMappings { get; set; }
        public virtual List<UserTestHistoryModel> UserTestHistories { get; set; }
    }
}
