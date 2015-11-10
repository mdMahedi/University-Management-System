using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyUniversityApp.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
    }
}