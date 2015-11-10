namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "RoomCode");
        }
    }
}
