using CourseEnroll.DataAccessLayer;
using CourseEnroll.Models.VM;
using CourseEnroll.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnroll.Controllers
{
    public class StudentController : Controller
    {
        public StudentGateway studentGateway { get; set; }
        public StudentController(IConfiguration configuration)
        {
            studentGateway = new StudentGateway(configuration);
        }
        public IActionResult Index()
        {
            List<Student> students = studentGateway.GetList();
            return View(students);
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            int res = studentGateway.Add(student);
            return Redirect("/Student");
        }
        [HttpGet]
        public ActionResult UpdateStudent(int id)
        {
            Student student = studentGateway.GetById(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult UpdateStudent(Student student)
        {
            int res = studentGateway.Update(student);
            return Redirect("/Student");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = studentGateway.Delete(id);
            return Redirect("/Student");
        }
    }
}
