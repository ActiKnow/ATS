namespace ATS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FEEDBACK_CHANGES : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFeedbacks", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.UserFeedbacks", "Reating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFeedbacks", "Reating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.UserFeedbacks", "Rating");
        }
    }
}
