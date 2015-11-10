namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssiginCourseToTeachers", "CraditTaken", c => c.Int(nullable: false));
            AddColumn("dbo.AssiginCourseToTeachers", "RemainingCradit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssiginCourseToTeachers", "RemainingCradit");
            DropColumn("dbo.AssiginCourseToTeachers", "CraditTaken");
        }
    }
}
