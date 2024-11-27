using System.ComponentModel;

namespace CourseEnroll.Models
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("Student Name")]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public string Education { get; set; }
        [DisplayName("Institute/University")]
        public string Institute { get; set; }
        [DisplayName("Registration Date")]
        public DateTime RegDate { get; set; }
        public bool IsActive { get; set; }
    }
}
