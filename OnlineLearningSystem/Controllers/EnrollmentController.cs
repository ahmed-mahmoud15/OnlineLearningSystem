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
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if (studentId != userId)
            {
                return Unauthorized();
            }
            try
            {
                await enrollmentService.EnrollInCourse(studentId, courseId);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction();
        }
    }
}
