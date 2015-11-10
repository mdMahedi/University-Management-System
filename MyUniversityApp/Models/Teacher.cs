using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyUniversityApp.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required, DisplayName("Teacher Name")]
        public string TeacherName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid Email address")]
        [Remote("IsEmailUnique", "Teacher", HttpMethod = "POST", ErrorMessage = "This Email is already registered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Contact No"), DisplayName("Contact No")]
        [Remote("IsContactUnique", "Teacher", HttpMethod = "POST", ErrorMessage = "This Phone no is already registered")]
        [RegularExpression(@"^\(?[+]([8]{2})([0]{1})([1]{1})([1-9]{1})([0-9]{8})$",
           ErrorMessage = "Invalid Phone number. Try this format (+88017XXXXXXXX)")]
        public string ContactNo { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Select your Designation"), DisplayName("Designation")]
        
        public int DesignationId { get; set; }


        [Required(ErrorMessage = "Select your Department"), DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required, DisplayName("Cradit To Be Taken")]
        [Range(6, 25, ErrorMessage = "Invalid Cradit")]
        public double TeacherCraditLimit { get; set; }

        
        public virtual double RemainingCradit { get; set; }
       
        public virtual double AssignedCradit { get; set; }

        public virtual Designation Designation { get; set; }
        public virtual Department Department { get; set; }

        





        public virtual ICollection<Course> Courses { get; set; }
    }


        //[HttpPost]
        //public JsonResult IsEmailUnique(string email)
        //{
        //    var result = db.Teachers.Count(u => u.Email == email) == 0;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult IsContactUnique(string contactno)
        //{
        //    var result = db.Teachers.Count(u => u.ContactNo == contactno) == 0;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult IsPositionAvailable(int designationid)
        //{
        //    //int count = db.Teachers.Count(t => t.DesignationId == designationid);
        //    //if (designationid == 1 && count == 1) return Json(false, JsonRequestBehavior.AllowGet);
        //    //if (designationid == 2 && count == 3) return Json(false, JsonRequestBehavior.AllowGet);
        //    //if (designationid == 3 && count == 8) return Json(false, JsonRequestBehavior.AllowGet);
        //    //if (designationid == 4 && count == 13) return Json(false, JsonRequestBehavior.AllowGet);
        //    //if (designationid == 5 && count == 10) return Json(false, JsonRequestBehavior.AllowGet);
           
        //    return Json(false, JsonRequestBehavior.AllowGet);
        //}
}