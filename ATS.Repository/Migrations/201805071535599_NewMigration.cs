namespace ATS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionBanks",
                c => new
                    {
                        QId = c.Guid(nullable: false),
                        Description = c.String(),
                        QuesTypeId = c.Guid(nullable: false),
                        LevelTypeId = c.Guid(nullable: false),
                        CategoryTypeId = c.Guid(nullable: false),
                        DefaultMark = c.Int(nullable: false),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.QId);
            
            CreateTable(
                "dbo.QuestionOptionMappings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QId = c.Guid(nullable: false),
                        OptionKeyId = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBanks", t => t.QId, cascadeDelete: true)
                .Index(t => t.QId);
            
            CreateTable(
                "dbo.TestQuestionMappings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TestBankId = c.Guid(nullable: false),
                        QId = c.Guid(nullable: false),
                        Marks = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBanks", t => t.QId, cascadeDelete: true)
                .ForeignKey("dbo.TestBanks", t => t.TestBankId, cascadeDelete: true)
                .Index(t => t.TestBankId)
                .Index(t => t.QId);
            
            CreateTable(
                "dbo.TestBanks",
                c => new
                    {
                        TestBankId = c.Guid(nullable: false),
                        CategoryTypeId = c.Guid(nullable: false),
                        LavelTypeId = c.Guid(nullable: false),
                        Description = c.String(),
                        Instructions = c.String(),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TestTypeId = c.Guid(nullable: false),
                        TotalMarks = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TestBankId);
            
            CreateTable(
                "dbo.TestAssignments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TestBankId = c.Guid(nullable: false),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TestBanks", t => t.TestBankId, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestBankId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FName = c.String(),
                        LName = c.String(),
                        Mobile = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        RoleTypeId = c.Guid(nullable: false),
                        UserTypeId = c.Guid(),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        TypeDef_TypeId = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.TypeDefs", t => t.TypeDef_TypeId)
                .Index(t => t.TypeDef_TypeId);
            
            CreateTable(
                "dbo.TypeDefs",
                c => new
                    {
                        TypeId = c.Guid(nullable: false),
                        Description = c.String(),
                        Value = c.String(),
                        ParentKey = c.String(),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.UserCredentials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoleTypeId = c.Guid(),
                        PrevPassword = c.String(),
                        CurrPassword = c.String(),
                        EmailId = c.String(),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserTestHistories",
                c => new
                    {
                        HistoryId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TestbankId = c.Guid(nullable: false),
                        AssignedDate = c.DateTime(nullable: false),
                        LastUsedDate = c.DateTime(),
                        IsFinished = c.Boolean(nullable: false),
                        ReusableDate = c.DateTime(nullable: false),
                        TotalDuration = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.TestBanks", t => t.TestbankId, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestbankId);
            
            CreateTable(
                "dbo.UserAttemptedHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        History_Id = c.Guid(nullable: false),
                        QId = c.Guid(nullable: false),
                        OptionSelected_Id = c.String(),
                        Description = c.String(),
                        UserTestHistory_HistoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBanks", t => t.QId, cascadeDelete: true)
                .ForeignKey("dbo.UserTestHistories", t => t.UserTestHistory_HistoryId)
                .Index(t => t.QId)
                .Index(t => t.UserTestHistory_HistoryId);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        KeyId = c.String(),
                        Description = c.String(),
                        StatusId = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestQuestionMappings", "TestBankId", "dbo.TestBanks");
            DropForeignKey("dbo.UserTestHistories", "UserId", "dbo.UserInfoes");
            DropForeignKey("dbo.UserAttemptedHistories", "UserTestHistory_HistoryId", "dbo.UserTestHistories");
            DropForeignKey("dbo.UserAttemptedHistories", "QId", "dbo.QuestionBanks");
            DropForeignKey("dbo.UserTestHistories", "TestbankId", "dbo.TestBanks");
            DropForeignKey("dbo.UserCredentials", "UserId", "dbo.UserInfoes");
            DropForeignKey("dbo.UserInfoes", "TypeDef_TypeId", "dbo.TypeDefs");
            DropForeignKey("dbo.TestAssignments", "UserId", "dbo.UserInfoes");
            DropForeignKey("dbo.TestAssignments", "TestBankId", "dbo.TestBanks");
            DropForeignKey("dbo.TestQuestionMappings", "QId", "dbo.QuestionBanks");
            DropForeignKey("dbo.QuestionOptionMappings", "QId", "dbo.QuestionBanks");
            DropIndex("dbo.UserAttemptedHistories", new[] { "UserTestHistory_HistoryId" });
            DropIndex("dbo.UserAttemptedHistories", new[] { "QId" });
            DropIndex("dbo.UserTestHistories", new[] { "TestbankId" });
            DropIndex("dbo.UserTestHistories", new[] { "UserId" });
            DropIndex("dbo.UserCredentials", new[] { "UserId" });
            DropIndex("dbo.UserInfoes", new[] { "TypeDef_TypeId" });
            DropIndex("dbo.TestAssignments", new[] { "TestBankId" });
            DropIndex("dbo.TestAssignments", new[] { "UserId" });
            DropIndex("dbo.TestQuestionMappings", new[] { "QId" });
            DropIndex("dbo.TestQuestionMappings", new[] { "TestBankId" });
            DropIndex("dbo.QuestionOptionMappings", new[] { "QId" });
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.UserAttemptedHistories");
            DropTable("dbo.UserTestHistories");
            DropTable("dbo.UserCredentials");
            DropTable("dbo.TypeDefs");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.TestAssignments");
            DropTable("dbo.TestBanks");
            DropTable("dbo.TestQuestionMappings");
            DropTable("dbo.QuestionOptionMappings");
            DropTable("dbo.QuestionBanks");
        }
    }
}
