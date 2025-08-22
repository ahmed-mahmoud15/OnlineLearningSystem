using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly IStudentService studentService;

        public EnrollmentController(IEnrollmentService enrollmentService, IStudentService studentService)
        {
            this.enrollmentService = enrollmentService;
            this.studentService = studentService;
        }

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

        public async Task<IActionResult> MyEnrollments()
        {
            int studentId = int.Parse(User?.FindFirst("UserId").Value);
            return View(await enrollmentService.GetEnrollmentsInfoAsync(studentId));
        }
    }
}
