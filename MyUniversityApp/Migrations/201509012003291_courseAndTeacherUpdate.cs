namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseAndTeacherUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "CourseDescription", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "TeacherCraditLimit", c => c.Double(nullable: false));
            AddColumn("dbo.Teachers", "RemainingCradit", c => c.Double(nullable: false));
            AddColumn("dbo.Teachers", "AssignedCradit", c => c.Double(nullable: false));
            DropColumn("dbo.Teachers", "CraditToBeTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "CraditToBeTaken", c => c.Double(nullable: false));
            DropColumn("dbo.Teachers", "AssignedCradit");
            DropColumn("dbo.Teachers", "RemainingCradit");
            DropColumn("dbo.Teachers", "TeacherCraditLimit");
            DropColumn("dbo.Courses", "CourseDescription");
            DropColumn("dbo.Courses", "CourseCode");
        }
    }
}
