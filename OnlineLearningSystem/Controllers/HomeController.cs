using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await courseService.GetAllCoursesAsync());
        }

    }
}
