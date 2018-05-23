namespace ATS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultMarkChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionBanks", "DefaultMark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionBanks", "DefaultMark", c => c.Int(nullable: false));
        }
    }
}
