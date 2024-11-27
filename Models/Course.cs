using System.ComponentModel;

namespace CourseEnroll.Models
{
    public class Course
    {
        public int Id { get; set; }
        [DisplayName("Course Name")]
        public string Name { get; set; }
        public string Description { get; set; }
       
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Course Fee")]
        public decimal CourseFee { get; set; }
        [DisplayName("Course Duration")]
        public string CourseDuration { get; set; }
        public bool IsActive { get; set; }
    }
}
