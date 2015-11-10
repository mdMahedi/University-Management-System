namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayNameCell : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "DayName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Days", "DayName");
        }
    }
}
