using System.ComponentModel;

namespace CourseEnroll.Models.VM
{
    public class CourseRegVM
    {

        public int Id { get; set; }
        [DisplayName("Ref No")]
        public string RefNo { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Total Fee")]
        public decimal TotalFee { get; set; }
        [DisplayName("Registration")]
        public DateTime CourseRegDate { get; set; }
        public bool IsActive { get; set; }

    }
}
