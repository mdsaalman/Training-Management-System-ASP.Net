using System.ComponentModel;

namespace CourseEnroll.Models
{
    public class CoursePayment
    {
        public int Id { get; set; }
        [DisplayName("Ref No")]
        public string RefNo { get; set; }
        [DisplayName("Student")]
        public int StudentId { get; set; }
        [DisplayName("Pay Amount")]
        public decimal PayAmount { get; set; }
        [DisplayName("Pay Date")]
        public DateTime PaymentDate { get; set; }
    }
}
