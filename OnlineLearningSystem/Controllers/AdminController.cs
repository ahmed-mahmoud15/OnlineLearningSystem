using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public AdminController()
        {
        }

        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
    }
}
