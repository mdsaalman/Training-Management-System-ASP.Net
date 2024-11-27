using System.ComponentModel;

namespace CourseEnroll.Models
{
    public class CourseReg
    {
        public int Id { get; set; }
        [DisplayName("Ref No")]
        public string RefNo { get; set; }
        [DisplayName("Student Name")]
        public int StudentId { get; set; }
        [DisplayName("Total Fee")]
        public decimal TotalFee { get; set; }
        [DisplayName("Registration Date")]
        public DateTime CourseRegDate { get; set; }
        public bool IsCancelled { get; set; }
    }

    public class CourseRegDetails
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [DisplayName("Student Name")]
        public int StudentId { get; set; }
        public decimal CourseFee { get; set; }
        [DisplayName("Course Reg Date")]
        public DateTime CourseRegDate { get; set; }
        public bool IsActive { get; set; }
    }

    
}
