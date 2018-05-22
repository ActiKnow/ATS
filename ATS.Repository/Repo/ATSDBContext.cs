using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using ATS.Repository.Migrations;
using ATS.Repository.Model;


namespace ATS.Repository.Repo
{
    public class ATSDBContext : DbContext
    {
        static ATSDBContext()
        {
            Database.SetInitializer(new ATSDBInitializer());
        }
        public ATSDBContext() : base("name=ATSDBContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ATSDBContext, ATS.Repository.Migrations.Configuration>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ATSDBContext>());
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
        public virtual DbSet<UserTestHistory> UserTestHistory { get; set; }
        public virtual DbSet<UserFeedback> UserFeedback { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuestionBank>()
                    .HasRequired(m => m.CategoryType)
                    .WithMany(t => t.QuestionCategories)
                    .HasForeignKey(m => m.CategoryTypeValue)
                    .WillCascadeOnDelete(false);
            modelBuilder.Entity<QuestionBank>()
                 .HasRequired(m => m.LevelType)
                 .WithMany(t => t.QuestionLevels)
                 .HasForeignKey(m => m.LevelTypeValue)
                 .WillCascadeOnDelete(false);
            modelBuilder.Entity<QuestionBank>()
                 .HasRequired(m => m.QuestionType)
                 .WithMany(t => t.QuestionTypes)
                 .HasForeignKey(m => m.QuesTypeValue)
                 .WillCascadeOnDelete(false);
        }

    }
}
