using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningSystem.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
