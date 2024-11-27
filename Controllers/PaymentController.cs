using CourseEnroll.DataAccessLayer;
using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseEnroll.Controllers
{

    public class PaymentController : Controller
    {
        public CourseRegGateway courseRegGateway { get; set; }
        public StudentGateway studentGateway { get; set; }
        public PaymentGateway paymentGateway { get; set; }
        public PaymentController(IConfiguration configuration)
        {
            paymentGateway = new PaymentGateway(configuration);
            studentGateway = new StudentGateway(configuration);
            courseRegGateway = new CourseRegGateway(configuration);
        }

        public IActionResult Index()
        {
            List<PaymentVM> payments = paymentGateway.GetList();
            return View(payments);
        }
        [HttpGet]
        public ActionResult AddPay()
        {
            List<Student> student = studentGateway.GetList();
            ViewBag.Student = student;
            return View();
        }
        [HttpPost]
        public ActionResult AddPay(CoursePayment pay)
        {
            int res = paymentGateway.Add(pay);
            return Redirect("/Payment");
        }

        [HttpGet]
        public ActionResult DetailsPay(int Id)
        {
            Student student = studentGateway.GetById(Id); 
            List<CoursePayment> payments = paymentGateway.PayList(Id);
            List<CourseVM> course = courseRegGateway.CourseList(Id);
            ViewBag.Student = student;
            ViewBag.Course = course;
            return View(payments);
        }
    }
}
