using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MyUniversityApp.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }


        [Required, DisplayName("Course Code")]
        [Remote("IsCourseCodeUnique", "Course", HttpMethod = "POST", ErrorMessage = "Course code already exist.")]
        public string CourseCode { get; set; }

        [Required, DisplayName("Course Name")]
        [Remote("IsCourseNameUnique", "Course", HttpMethod = "POST", ErrorMessage = "Course name already exist.")]
        public string CourseName { get; set; }

        [Required, DisplayName("Description"), DataType(DataType.MultilineText)]
        public string CourseDescription { get; set; }


        [Required, DisplayName("Department")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }

        
        [Required, DisplayName("Semister")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select a semister")]
        public int SemisterId { get; set; }

        [Required, Range(1 , 4, ErrorMessage = "Invalid Cradit")]
        public double Cradit { get; set; }


        public virtual Department Department { get; set; }
        public virtual Semister Semister { get; set; }
    }

    //[HttpPost]
    //    public JsonResult IsCourseNameUnique(string coursename)
    //    {
    //        var result = db.Courses.Count(u => u.CourseName == coursename) == 0;
    //        return Json(result, JsonRequestBehavior.AllowGet);
    //    }

    public class AssiginCourseToStudent
    {
        [Key]
        public int AssiginCourseToStudentId { get; set; }

        [Required, DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required, DisplayName("Semister")]
        public int SemisterId { get; set; }

        [Required, DisplayName("Course")]
        public int CourseId { get; set; }

        [Required, DisplayName("Student")]
        public int StudentId { get; set; }



        public virtual Department Department { get; set; }
        public virtual Semister Semister { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }

    public class AssiginCourseToTeacher
    {
        [Key]
        public int AssiginCourseToTeacherId { get; set; }

        [Required, DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required, DisplayName("Teacher")]
        public int TeacherId { get; set; }

        

        [Required, DisplayName("Course")]
        public int CourseId { get; set; }



        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}