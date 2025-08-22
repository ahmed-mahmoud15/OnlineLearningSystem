using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IEnrollmentService enrollmentService;
        private readonly ICategoryService categoryService;
        private readonly ILikeService likeService;



        public CourseController(ICategoryService categoryService, ICourseService courseService, IEnrollmentService enrollmentService, ILikeService likeService)
        {
            this.courseService = courseService;
            this.enrollmentService = enrollmentService;
            this.categoryService = categoryService;
            this.likeService = likeService;
        }

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddCourse(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (id != userId)
            {
                return Unauthorized();
            }
            ViewBag.Categories = await categoryService.GetCategoriesWithCoursesAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddCourse(AddCourseViewModel model)
        {
            try
            {
                await courseService.AddCourseAsync(model);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            return RedirectToAction("MyProfile", "Instructor");
        }

        [HttpGet]
        public async Task<IActionResult> CourseDetails(int id) {
            bool isEnrolled = false;
            bool isLiked = false;
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value);
                isEnrolled = await enrollmentService.IsStudentEnrolledInCourseAsync(userId, id);
                isLiked = await likeService.IsStudentLikedCourse(userId, id);
            }
            ViewBag.IsEnrolled = isEnrolled;
            ViewBag.IsLiked = isLiked;
            return View(await courseService.GetCourseDetailsAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> ShowCourses(int count = 10, int page = 1)
        {
            return View(await courseService.GetAllCoursesAsync());
        }
    }
}
