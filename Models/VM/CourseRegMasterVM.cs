using System.ComponentModel;

namespace CourseEnroll.Models.VM
{
    public class CourseRegMasterVM
    {
       

        public CourseReg CourseReg { get; set; }
        public List<CourseRegDetails> CourseRegDetails { get; set; }
        public CourseRegMasterVM()
        {
            CourseReg = new CourseReg();
            CourseRegDetails = new List<CourseRegDetails>();
        }

    }
}
