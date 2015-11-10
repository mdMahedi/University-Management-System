namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteACell : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Days", "DateName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Days", "DateName", c => c.String(nullable: false));
        }
    }
}
