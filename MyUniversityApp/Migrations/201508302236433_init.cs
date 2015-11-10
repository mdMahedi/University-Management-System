namespace MyUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssiginCourseToStudents",
                c => new
                    {
                        AssiginCourseToStudentId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        SemisterId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssiginCourseToStudentId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Semisters", t => t.SemisterId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemisterId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        SemisterId = c.Int(nullable: false),
                        CourseName = c.String(nullable: false),
                        Cradit = c.Int(nullable: false),
                        Student_StudentId = c.Int(),
                        Teacher_TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherId)
                .ForeignKey("dbo.Semisters", t => t.SemisterId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemisterId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Teacher_TeacherId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentCode = c.String(nullable: false),
                        DepartmentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false),
                        StudentReg = c.String(nullable: false),
                        EnrollmentDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SemisterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Semisters", t => t.SemisterId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemisterId);
            
            CreateTable(
                "dbo.Semisters",
                c => new
                    {
                        SemisterId = c.Int(nullable: false, identity: true),
                        SemisterName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SemisterId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(),
                        DesignationId = c.Int(nullable: false),
                        Student_StudentId = c.Int(),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Designations", t => t.DesignationId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.DesignationId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.AssiginCourseToTeachers",
                c => new
                    {
                        AssiginCourseToTeacherId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        SemisterId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssiginCourseToTeacherId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Semisters", t => t.SemisterId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemisterId)
                .Index(t => t.CourseId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssiginCourseToTeachers", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.AssiginCourseToTeachers", "SemisterId", "dbo.Semisters");
            DropForeignKey("dbo.AssiginCourseToTeachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AssiginCourseToTeachers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AssiginCourseToStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AssiginCourseToStudents", "SemisterId", "dbo.Semisters");
            DropForeignKey("dbo.AssiginCourseToStudents", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AssiginCourseToStudents", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SemisterId", "dbo.Semisters");
            DropForeignKey("dbo.Teachers", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Teachers", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Courses", "Teacher_TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "SemisterId", "dbo.Semisters");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Rooms", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Results", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.AssiginCourseToTeachers", new[] { "TeacherId" });
            DropIndex("dbo.AssiginCourseToTeachers", new[] { "CourseId" });
            DropIndex("dbo.AssiginCourseToTeachers", new[] { "SemisterId" });
            DropIndex("dbo.AssiginCourseToTeachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "Student_StudentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Students", new[] { "SemisterId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Rooms", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Results", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Courses", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.Courses", new[] { "Student_StudentId" });
            DropIndex("dbo.Courses", new[] { "SemisterId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.AssiginCourseToStudents", new[] { "StudentId" });
            DropIndex("dbo.AssiginCourseToStudents", new[] { "CourseId" });
            DropIndex("dbo.AssiginCourseToStudents", new[] { "SemisterId" });
            DropIndex("dbo.AssiginCourseToStudents", new[] { "DepartmentId" });
            DropTable("dbo.AssiginCourseToTeachers");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.Semisters");
            DropTable("dbo.Students");
            DropTable("dbo.Rooms");
            DropTable("dbo.Results");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.AssiginCourseToStudents");
        }
    }
}
