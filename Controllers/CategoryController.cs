using CourseEnroll.DataAccessLayer;
using CourseEnroll.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnroll.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryGateway categoryGateway { get; set; }
        public CategoryController(IConfiguration configuration)
        {
            categoryGateway = new CategoryGateway(configuration);
        }

        public IActionResult Index()
        {
            List<Category> category = categoryGateway.GetList();
            return View(category);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            int res = categoryGateway.Add(category);
          
            return Redirect("/Category");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            Category category = categoryGateway.GetById(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            int res = categoryGateway.Update(category);
            return Redirect("/Category");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = categoryGateway.Delete(id);
            return Redirect("/Category");
        }
    }
}
