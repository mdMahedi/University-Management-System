namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomAllocations", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomAllocations", "CourseId");
            AddForeignKey("dbo.RoomAllocations", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomAllocations", "CourseId", "dbo.Courses");
            DropIndex("dbo.RoomAllocations", new[] { "CourseId" });
            DropColumn("dbo.RoomAllocations", "CourseId");
        }
    }
}
