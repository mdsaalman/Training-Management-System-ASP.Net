using System.ComponentModel;

namespace CourseEnroll.Models.VM
{
    public class PaymentVM
    {
        public int Id { get; set; }
        [DisplayName("Student Name")]
        public string Name { get; set; }
        [DisplayName("Total Fee")]
        public decimal TotalFee { get; set; }
        [DisplayName("Paid")]
        public decimal Paid { get; set; }
        [DisplayName("Due")]
        public decimal Due { get; set; }
    }
}
