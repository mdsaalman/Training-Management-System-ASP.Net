using CourseEnroll.DataAccessLayer;
using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace CourseEnroll.Controllers
{
    public class CourseRegController : Controller
    {
        public CourseRegGateway courseRegGateway { get; set; }
        public StudentGateway studentGateway { get; set; }
        public CourseGateway courseGateway { get; set; }
        public CourseRegController(IConfiguration configuration)
        {
            courseRegGateway = new CourseRegGateway(configuration);
            studentGateway = new StudentGateway(configuration);
            courseGateway = new CourseGateway(configuration);
        }
        public IActionResult Index()
        {
            List<CourseRegVM> regList = courseRegGateway.GetList();
            return View(regList);
        }
        [HttpGet]
        public ActionResult AddCourseReg()
        {
            List<Student> student = studentGateway.GetList();
            List<CourseVM> course = courseGateway.GetList();
            CourseRegMasterVM courseRegMasterVM = new CourseRegMasterVM();
           
            ViewBag.Student = student;
            ViewBag.Course = course;
            return View(courseRegMasterVM);
        }

        [HttpPost]
        public ActionResult AddCourseReg(CourseRegMasterVM courseRegMasterVM)
        {
            List<Student> student = studentGateway.GetList();
            List<CourseVM> course = courseGateway.GetList();
            ViewBag.Student = student;
            ViewBag.Course = course;
            if (!ModelState.IsValid)
            {
                return View(courseRegMasterVM);
            }

            int result = courseRegGateway.Add(courseRegMasterVM);
            if (result == 1)
            {
                ViewBag.Message = "Saved";
            }
            return View(new CourseRegMasterVM());
        }

        [HttpGet]
        public ActionResult UpdateCourseReg(int id)
        {
            List<Student> student = studentGateway.GetList();
            List<CourseVM> course = courseGateway.GetList();
            ViewBag.Student = student;
            ViewBag.Course = course;
            CourseRegMasterVM courseRegMasterVM = courseRegGateway.GetById(id);
            return View(courseRegMasterVM);
        }
        [HttpPost]
        public ActionResult UpdateCourseReg(Course course)
        {
            int res = courseRegGateway.Update(course);
            return Redirect("/CourseReg");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            int result = courseRegGateway.Delete(Id);
            return Redirect("/CourseReg");
        }
    }
}
