using System.ComponentModel;

namespace CourseEnroll.Models.VM
{
    public class CourseVM
    {
        public int Id { get; set; }
        [DisplayName("Course Name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Course Duration")]
        public string CourseDuration { get; set; }
        [DisplayName("Course Fee")]
        public decimal CourseFee { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
        [DisplayName("Status")]
        public bool IsActive { get; set; }
    }
}
