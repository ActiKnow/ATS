using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using ATS.Repository.Model;


namespace ATS.Repository.DAO
{
    public class ATSDBContext : DbContext
    {
        public ATSDBContext() : base("name=ATSDBContext")
        {

        }

        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<QuestionBank> QuestionBank { get; set; }
        public virtual DbSet<QuestionOption> QuestionOption { get; set; }
        public virtual DbSet<QuestionOptionMapping> QuestionOptionMapping { get; set; }
        public virtual DbSet<TestAssignment> TestAssignment { get; set; }
        public virtual DbSet<TestBank> TestBank { get; set; }
        public virtual DbSet<TestQuestionMapping> TestQuestionMapping { get; set; }
        public virtual DbSet<TypeDef> TypeDef { get; set; }
        public virtual DbSet<UserAttemptedHistory> UserAttemptedHistory { get; set; }
        public virtual DbSet<UserCredential> UserCredential { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<UserTestHistory> UserTestHistory { get; set; }
       
    }
}
