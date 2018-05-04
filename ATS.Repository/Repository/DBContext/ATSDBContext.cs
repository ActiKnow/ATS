using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Repository.DBContext
{
    public class ATSDBContext : DbContext
    {
        public ATSDBContext() : base("name=ATSDBContext")
        {

        }

        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<QuestionBank> QuestionBanks { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<QuestionOptionMapping> QuestionOptionMappings { get; set; }
        public virtual DbSet<TestAssignment> TestAssignments { get; set; }
        public virtual DbSet<TestBank> TestBanks { get; set; }
        public virtual DbSet<TestQuestionMapping> TestQuestionMappings { get; set; }
        public virtual DbSet<TypeDef> TypeDefs { get; set; }
        public virtual DbSet<UserAttemptedHistory> UserAttemptedHistories { get; set; }
        public virtual DbSet<UserCredential> UserCredentials { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<UserTestHistory> UserTestHistories { get; set; }
    }
}
