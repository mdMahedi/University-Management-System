using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace MyUniversityApp.Models
{
    public class Department
    {
        [Key]
        [DisplayName("Id")]
        public int DepartmentId { get; set; }

        [Required, DisplayName("Code")]
        [Remote("CodeIsExists", "Department", HttpMethod = "POST", ErrorMessage = "This Department Code is Already Exist")]
        public string DepartmentCode { get; set; }

        [Required, DisplayName("Name")]
        [Remote("NameIsExists", "Department", HttpMethod = "POST", ErrorMessage = "This Department Name is Already Exist")]
        public string DepartmentName { get; set; }


        public virtual  ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        
    }
}