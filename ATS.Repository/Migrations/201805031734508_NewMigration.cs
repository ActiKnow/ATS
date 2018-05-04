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
                        QuesTypeId = c.String(),
                        LevelTypeId = c.String(),
                        DefaultMark = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
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
                        Id = c.String(nullable: false, maxLength: 128),
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
                        CategoryTypeId = c.String(),
                        LavelTypeId = c.String(),
                        Description = c.String(),
                        Instructions = c.String(),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TestTypeId = c.String(),
                        TotalMarks = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
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
                        Status = c.Boolean(nullable: false),
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
                        UserTypeId = c.Guid(nullable: false),
                        Status = c.Boolean(nullable: false),
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
                        ParentTypeId = c.String(),
                        Status = c.Boolean(nullable: false),
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
                        RoleId = c.Guid(),
                        PrevPassword = c.String(),
                        CurrPassword = c.String(),
                        EmailId = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        USERID = c.Guid(nullable: false),
                        ROLEID = c.Guid(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        LastUpdatedDate = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInfoes", t => t.USERID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.ROLEID, cascadeDelete: true)
                .Index(t => t.USERID)
                .Index(t => t.ROLEID);
            
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
                        Status = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.UserRoleMappings", "ROLEID", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoleMappings", "USERID", "dbo.UserInfoes");
            DropForeignKey("dbo.UserCredentials", "RoleId", "dbo.UserRoles");
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
            DropIndex("dbo.UserRoleMappings", new[] { "ROLEID" });
            DropIndex("dbo.UserRoleMappings", new[] { "USERID" });
            DropIndex("dbo.UserCredentials", new[] { "RoleId" });
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
            DropTable("dbo.UserRoleMappings");
            DropTable("dbo.UserRoles");
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
