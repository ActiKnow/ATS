namespace ATS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuesRefMigration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.QuestionBanks", "QuesTypeValue");
            CreateIndex("dbo.QuestionBanks", "LevelTypeValue");
            CreateIndex("dbo.QuestionBanks", "CategoryTypeValue");
            AddForeignKey("dbo.QuestionBanks", "CategoryTypeValue", "dbo.TypeDefs", "Value");
            AddForeignKey("dbo.QuestionBanks", "LevelTypeValue", "dbo.TypeDefs", "Value");
            AddForeignKey("dbo.QuestionBanks", "QuesTypeValue", "dbo.TypeDefs", "Value");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionBanks", "QuesTypeValue", "dbo.TypeDefs");
            DropForeignKey("dbo.QuestionBanks", "LevelTypeValue", "dbo.TypeDefs");
            DropForeignKey("dbo.QuestionBanks", "CategoryTypeValue", "dbo.TypeDefs");
            DropIndex("dbo.QuestionBanks", new[] { "CategoryTypeValue" });
            DropIndex("dbo.QuestionBanks", new[] { "LevelTypeValue" });
            DropIndex("dbo.QuestionBanks", new[] { "QuesTypeValue" });
        }
    }
}
