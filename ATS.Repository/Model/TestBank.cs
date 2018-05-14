using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int CategoryTypeValue { get; set; }
        public int LevelTypeValue { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public decimal Duration { get; set; }
        public int TestTypeValue { get; set; }
        public decimal TotalMarks { get; set; }
        public List<TestAssignment> TestAssignments { get; set; }
        public List<TestQuestionMapping> TestQuestionMappings { get; set; }
        public List<UserTestHistory> UserTestHistories { get; set; }

        //[ForeignKey("CategoryTypeValue")]
        //public TypeDef CategoryType { get; set; }

        //[ForeignKey("LavelTypeValue")]
        //public TypeDef LevelType { get; set; }

        //[ForeignKey("TestTypeValue")]
        //public TypeDef TestType { get; set; }
    }
}
