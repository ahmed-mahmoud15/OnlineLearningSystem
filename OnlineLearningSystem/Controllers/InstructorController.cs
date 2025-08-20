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

        public InstructorController(IInstructorService instructorService, ICategoryService categoryService)
        {
            this.instructorService = instructorService;
            this.categoryService = categoryService;
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
                return NotFound(ex.InnerException.Message);
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
                return NotFound(ex.InnerException.Message);
            }
            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public async Task<IActionResult> AddCourse(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (id != userId)
            {
                return Unauthorized();
            }
            ViewBag.Categories = await categoryService.GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseViewModel model)
        {
            try
            {
                await instructorService.AddCourseAsync(model);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.InnerException.Message);
            }
            return RedirectToAction("MyProfile");
        }
    }
}
