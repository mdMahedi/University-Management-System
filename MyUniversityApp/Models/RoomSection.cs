using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniversityApp.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string RoomCode { get; set; }
    }

    public class RoomAllocation
    {
        [Key]
        public int RoomAllocationId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int DayId { get; set; }

        public string Schedule { get; set; }

        [Required, DataType(DataType.Time, ErrorMessage = "Must be a time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [NotMapped]
        public TimeSpan FromTime { get; set; }

        [NotMapped]
        public string FromAmPm { get; set; }

        [Required, DataType(DataType.Time ,ErrorMessage = "Must be a time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [NotMapped]
        public TimeSpan ToTime { get; set; }

        [NotMapped]
        public string ToAmPm { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("DayId")]
        public virtual Day Day { get; set; }

    }

    public class Day
    {
        [Key]
        public int DayId { get; set; }
        [Required]
        public string DayName { get; set; }
    }
}