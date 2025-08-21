using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int courseId, string returnUrl = null)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                await likeService.LikeCourse(userId, courseId);
                TempData["AlertMessage"] = "You have successfully liked this course.";
                TempData["AlertType"] = "success";
            }
            catch (ArgumentNullException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }
            catch (InvalidOperationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "danger";
            }

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Dislike(int courseId, string returnUrl = null)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                await likeService.DislikeCourse(userId, courseId);
                TempData["AlertMessage"] = "You have successfully dislike this course.";
                TempData["AlertType"] = "success";
            }
            catch (ArgumentNullException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }
            catch (InvalidOperationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "danger";
            }
            return LocalRedirect(returnUrl);
        }
    }
}
