namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "Department_DepartmentId" });
            RenameColumn(table: "dbo.Teachers", name: "Department_DepartmentId", newName: "DepartmentId");
            AddColumn("dbo.Teachers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "ContactNo", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "CraditToBeTaken", c => c.Double(nullable: false));
            AlterColumn("dbo.Courses", "Cradit", c => c.Double(nullable: false));
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teachers", "DepartmentId");
            AddForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            AlterColumn("dbo.Teachers", "DepartmentId", c => c.Int());
            AlterColumn("dbo.Teachers", "TeacherName", c => c.String());
            AlterColumn("dbo.Courses", "Cradit", c => c.Int(nullable: false));
            DropColumn("dbo.Teachers", "CraditToBeTaken");
            DropColumn("dbo.Teachers", "Address");
            DropColumn("dbo.Teachers", "ContactNo");
            DropColumn("dbo.Teachers", "Email");
            RenameColumn(table: "dbo.Teachers", name: "DepartmentId", newName: "Department_DepartmentId");
            CreateIndex("dbo.Teachers", "Department_DepartmentId");
            AddForeignKey("dbo.Teachers", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
        }
    }
}
