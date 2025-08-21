using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService instructorService;
        private readonly IFollowService followService;

        public InstructorController(IInstructorService instructorService, IFollowService followService)
        {
            this.instructorService = instructorService;
            this.followService = followService;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowAll()
        {
            return View(await instructorService.GetInstructorsInfoAsync());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Profile(int id)
        {
            InstructorProfileViewModel model;
            bool isFollowing = false;
            bool isAuthinticated = false;

            try
            {
                model = await instructorService.GetInstructorProfileAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }

            if (User.IsInRole("Student"))
            {
                var claim = User.FindFirst("UserId");
                if (claim != null && int.TryParse(claim.Value, out int userId))
                {
                    isFollowing = await followService.IsStudentFollowingInstructor(userId, id);
                    isAuthinticated = true;
                }
                else
                {
                    isAuthinticated = false;
                }
            }

            

            ViewBag.IsFollowing = isFollowing;
            ViewBag.IsAuthinticated = isAuthinticated;
            return View(model);
        }
    }
}
