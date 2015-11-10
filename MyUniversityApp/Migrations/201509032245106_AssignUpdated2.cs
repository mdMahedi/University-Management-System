namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignUpdated2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AssiginCourseToTeachers", "CraditTaken");
            DropColumn("dbo.AssiginCourseToTeachers", "RemainingCradit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssiginCourseToTeachers", "RemainingCradit", c => c.Int(nullable: false));
            AddColumn("dbo.AssiginCourseToTeachers", "CraditTaken", c => c.Int(nullable: false));
        }
    }
}
