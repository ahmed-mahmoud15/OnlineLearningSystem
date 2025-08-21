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

        public async Task<IActionResult> ManageStudents()
        {
            return View();
        }

        public async Task<IActionResult> ManageInstructors()
        {
            return View();
        }

        public async Task<IActionResult> ManageCourses()
        {
            return View();
        }

        public async Task<IActionResult> ManageCategories()
        {
            return View();
        }

        public async Task<IActionResult> BanUser(int id, string returnUrl)
        {
            TempData["AlertMessage"] = "Not Implemented Yet";
            TempData["AlertType"] = "danger";
            return LocalRedirect(returnUrl);
        }
    }
}
