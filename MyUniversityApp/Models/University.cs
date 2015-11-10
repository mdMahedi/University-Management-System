using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MyUniversityApp.Models
{
    public class University
    {
       
        public int DepartmentId { get; set; }
        public int SemisterId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public int StudentId { get; set; }
        public int ResultId { get; set; }


        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Semister> Semisters { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }

    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }

        public string DesignationName { get; set; }

    }

    public class Semister
    {
        [Key]
        public int SemisterId { get; set; }
        [Required]
        public string SemisterName { get; set; }
    }

    public class UniversityDBcontext : DbContext
    {
        public UniversityDBcontext()
            : base("MyUniversity")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<AssiginCourseToStudent> AssiginCourseToStudents { get; set; }
        public DbSet<AssiginCourseToTeacher> AssiginCourseToTeachers { get; set; }
        public DbSet<Semister> Semisters { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
        public DbSet<Day> Days { get; set; }
    }
}