using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService instructorService;
        private readonly ICategoryService categoryService;
        private readonly ICourseService courseService;

        public InstructorController(IInstructorService instructorService, ICategoryService categoryService, ICourseService courseService)
        {
            this.instructorService = instructorService;
            this.categoryService = categoryService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> MyProfile()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            InstructorProfileViewModel model;
            try
            {
                model = await instructorService.GetInstructorProfileAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (id != userId)
            {
                return Unauthorized();
            }
            return View(await instructorService.GetInstructorEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditInstructorViewModel model)
        {
            try
            {
                await instructorService.EditInstructorAsync(model);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            return RedirectToAction("MyProfile");
        }
    }
}
