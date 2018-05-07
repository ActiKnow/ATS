using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
    public class TestBank : BaseModel
    {
        public TestBank()
        {
            this.TestAssignments = new List<TestAssignment>();
            this.TestQuestionMappings = new List<TestQuestionMapping>();
            this.UserTestHistories = new List<UserTestHistory>();
        }
        [Key]
        public System.Guid TestBankId { get; set; }
        public string CategoryTypeId { get; set; }
        public string LavelTypeId { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public decimal Duration { get; set; }
        public string TestTypeId { get; set; }
        public decimal TotalMarks { get; set; }
        public virtual List<TestAssignment> TestAssignments { get; set; }
        public virtual List<TestQuestionMapping> TestQuestionMappings { get; set; }
        public virtual List<UserTestHistory> UserTestHistories { get; set; }
    }
}
