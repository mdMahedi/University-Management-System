namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "SemisterId", "dbo.Semisters");
            DropIndex("dbo.Students", new[] { "SemisterId" });
            AddColumn("dbo.Students", "StudentAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "StudentReg", c => c.String());
            DropColumn("dbo.Students", "SemisterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "SemisterId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "StudentReg", c => c.String(nullable: false));
            DropColumn("dbo.Students", "StudentAddress");
            CreateIndex("dbo.Students", "SemisterId");
            AddForeignKey("dbo.Students", "SemisterId", "dbo.Semisters", "SemisterId");
        }
    }
}
