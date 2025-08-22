using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IEnrollmentService enrollmentService;

        public StudentController(IStudentService studentService, IEnrollmentService enrollmentService)
        {
            this.studentService = studentService;
            this.enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> MyProfile()
        {
            int id = int.Parse(User.FindFirst("UserId")?.Value);
            StudentProfileViewModel model;
            try
            {
                model = await studentService.GetStudentProfileAsync(id);
            }catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            if(id != userId)
            {
                return Unauthorized();
            }
            return View(await studentService.GetStudentEditAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditStudentViewModel model)
        {
            try
            {
                await studentService.EditStudentAsync(model);
            }catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            return RedirectToAction("MyProfile");
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLesson(int courseId, int lessonId, string returnUrl = null)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                int seq = await enrollmentService.CompleteLessonAsync(userId, courseId, lessonId);
                TempData["AlertMessage"] = "You have successfully Completed this lesson";
                TempData["AlertType"] = "success";
                return RedirectToAction("ViewLesson", "Lesson", new { courseId = courseId, seqNum = seq });
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
