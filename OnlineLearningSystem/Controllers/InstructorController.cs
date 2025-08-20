using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            this.instructorService = instructorService;
        }

        public async Task<IActionResult> MyProfile()
        {
            return View();
        }
    }
}
