using CourseEnroll.DataAccessLayer;
using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnroll.Controllers
{
    public class CourseController : Controller
    {

        public CourseGateway courseGateway { get; set; }
        public CourseController(IConfiguration configuration)
        {
            courseGateway = new CourseGateway(configuration);
        }
        public IActionResult Index()
        {
            List<CourseVM> items = courseGateway.GetList();
            return View(items);
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            List<Category> category = courseGateway.GetCategoryList();
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(Course item)
        {
            int res = courseGateway.Add(item);
            return Redirect("/Course");
        }
        [HttpGet]
        public ActionResult UpdateCourse(int id)
        {
            List<Category> category = courseGateway.GetCategoryList();
            Course course = courseGateway.GetById(id);
            ViewBag.Category = category;
            return View(course);
        }
        [HttpPost]
        public ActionResult UpdateCourse(Course course)
        {
            int res = courseGateway.Update(course);
            return Redirect("/Course");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = courseGateway.Delete(id);
            return Redirect("/Course");
        }
    }
}
