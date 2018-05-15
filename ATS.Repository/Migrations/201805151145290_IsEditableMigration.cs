namespace ATS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEditableMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeDefs", "IsEditable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeDefs", "IsEditable");
        }
    }
}
