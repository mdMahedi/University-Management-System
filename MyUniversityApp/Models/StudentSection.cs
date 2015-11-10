using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyUniversityApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [DisplayName("Student Name"), Required]
        public string StudentName { get; set; }

        public virtual string StudentReg { get; set; }


        [Required, EmailAddress(ErrorMessage = "Invalid email Address")]
        [Remote("IsEmailUnique", "Student", HttpMethod = "POST",
            ErrorMessage = "This email already registered for another Student.")]
        public string Email { get; set; }


        [Required, DisplayName("Contact No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?[+]([8]{2})([0]{1})([1]{1})([1-9]{1})([0-9]{8})$",
            ErrorMessage = "Invalid Phone number. Try this format (+88017XXXXXXXX)")]
        [Remote("IsContactNoUnique", "Student", HttpMethod = "POST",
            ErrorMessage = "This Phone No already registered for another Student.")]
        public string ContactNo { get; set; }


        [Required, DisplayName("EnrollMent Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }


        [Required, DisplayName("Address"), DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }


        [DisplayName("Department"), Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }


        //[HttpPost]
        //public JsonResult CheckStudentName(string studentreg)
        //{
        //    var reg = db.Students.Count(u => u.StudentReg == studentreg) == 0;
        //    return Json(reg, JsonRequestBehavior.AllowGet);
        //}

        //public string GetMd5Hash(string textToHash)
        //{
        //    if (textToHash == String.Empty || textToHash.Length == 0) return "";
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] textToHashBytes = Encoding.Default.GetBytes(textToHash);
        //    byte[] hashResult = md5.ComputeHash(textToHashBytes);
        //    return BitConverter.ToString(hashResult);
        //}

        //[HttpPost]
        //public JsonResult IsRegUnique(string studentreg)
        //{
        //    var result = db.Students.Count(u => u.StudentReg == studentreg) == 0;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }

    public class StudentPromotion
    {
        [Key]
        public int StudentPromotionId { get; set; }
    }
}

    