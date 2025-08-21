using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }
        
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll(int studentId, int courseId, string returnUrl = null)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (studentId != userId)
            {
                return Unauthorized();
            }
            try
            {
                await enrollmentService.EnrollInCourse(studentId, courseId);
                TempData["AlertMessage"] = "You have successfully enrolled in this course.";
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
