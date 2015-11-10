namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssiginCourseToTeachers", "SemisterId", "dbo.Semisters");
            DropIndex("dbo.AssiginCourseToTeachers", new[] { "SemisterId" });
            DropColumn("dbo.AssiginCourseToTeachers", "SemisterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssiginCourseToTeachers", "SemisterId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssiginCourseToTeachers", "SemisterId");
            AddForeignKey("dbo.AssiginCourseToTeachers", "SemisterId", "dbo.Semisters", "SemisterId", cascadeDelete: true);
        }
    }
}
